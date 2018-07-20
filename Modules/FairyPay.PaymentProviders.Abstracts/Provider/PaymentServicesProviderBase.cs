using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using FairyPay.PaymentProviders;
using FairyPay.PaymentProviders.Context;
using FairyPay.PaymentProviders.Mapping;
using FairyPay.PaymentProviders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace FairyPay.PaymentProviders.Provider
{
    public abstract class PaymentServicesProviderBase : IPaymentServicesProvider
    {
        public ILogger Logger { get; set; }
        public Encoding Encoding { get; protected set; } = Encoding.UTF8;


        protected abstract string GateWay { get; }
        public ProviderSettings Settings { get; }

        protected IServiceProvider ServiceProvider { get; }

        protected PaymentServicesProviderBase(ProviderSettings settings, IServiceProvider serviceProvider)
        {
            Settings = settings;
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(this.GetType())) as ILogger;
        }

        public virtual string GenerateOrderId()
        {
            //19 bit
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 99999);
        }

        public virtual decimal ConvertAmount(decimal amount)
        {
            return Convert.ToDecimal(amount.ToString("#0.00"));
        }

        protected virtual string HttpMethod { get; } = HttpMethods.Post;
        protected abstract string SignFieldName { get; }
        public Meta Meta { get; set; }

        #region 接收回调
        public ReceiveContext Receive(bool isServerNotify, CallbackType type)
        {
            var httpContext = ServiceProvider.GetService<IHttpContextAccessor>().HttpContext;
            var receiveMapper = new ReceiveMapper();
            var receiveValues =
                CreateReceive(httpContext.Request, receiveMapper, ref isServerNotify);
            receiveMapper.Add(receiveValues);
            AttachReceive(receiveMapper);

            var result = new ReceiveContext()
            {
                Meta = Meta,
                IsServerNotify = isServerNotify
            };

            if (!VerifySign(receiveValues))
            {
                result.Model.Result = ResultType.SignError;
                //日志签名错误

                return result;
            }

            if ((result.Model.Result = SelectResult(receiveMapper[ReceiveMapField.Result])) == ResultType.Success)
            {
                //通过验证

                result.Model.OrderId = receiveMapper[ReceiveMapField.OrderId];
                result.Model.ExtParams = receiveMapper[ReceiveMapField.ExtParams];
                result.Model.TradeOrderId = receiveMapper[ReceiveMapField.TradeOrderId];
                result.Model.BankOrderId = receiveMapper[ReceiveMapField.BankOrderId];
                if (string.IsNullOrWhiteSpace(result.Model.ExtParams) &&
                    string.IsNullOrWhiteSpace(result.Model.OrderId))
                {
                    throw new ArgumentException("扩展参数和订单号不能同时为空，请设置接收映射");
                }

                result.Model.Amount = Convert.ToDecimal(receiveMapper[ReceiveMapField.Amount]);
                /*if (type == CallbackType.Private)
                {
                    using (WorkContext.Resolve<ITypedLockerHolder>().Lock<PaymentRecord>(result.Model.OrderId, 20 * 1000))
                    {
                        Finish(result);
                    }
                }
                else
                {
                    WorkContext.Resolve<IEnumerable<IPaymentEventHandler>>()
                        .Invoke(e => e.ReveiveSuccess(result), Logger);
                }*/
            }
            else
            {

            }
            //if (IsServerNotify(httpContext))
            if (isServerNotify)
            {
                //httpContext.Response.Clear();
                ServerNotifyResponse(result, httpContext.Response);
                //httpContext.Response.End();
                //Logger.Information("单号{0},服务端通知{1}", result.Model.OrderId, result.Model.Result);
            }
            else
            {
                //Logger.Information("单号{0},客户端通知{1}", result.Model.OrderId, result.Model.Result);
            }
            return result;

        }

        protected abstract void AttachReceive(IReceiveMap<ReceiveMapField> receiveMapper);

        protected virtual NameValueCollection CreateReceive(HttpRequest httpRequest, IMap<ReceiveMapField> receiveMapper, ref bool isServerNotify)
        {
            var receiveValues = (httpRequest.Method == HttpMethods.Post ? httpRequest.Form.AsNameValueCollection() : httpRequest.Query.AsNameValueCollection());

            return receiveValues;
        }

        protected virtual void ServerNotifyResponse(ReceiveContext context, HttpResponse httpResponse)
        {
        }
        protected abstract ResultType SelectResult(string result);
        protected abstract bool VerifySign(NameValueCollection receiveParams);
        #endregion

        #region 请求支付

        public RequestContext Request(PayRequestModel mode)
        {
            return CreateRequest(mode, new RequestMapper());
        }


        protected virtual RequestContext CreateRequest(PayRequestModel requestModel, IRequestMap<RequestMapField> requestMapper)
        {
            //设置订单号
            if (string.IsNullOrWhiteSpace(requestModel.OrderId))
            {
                requestModel[RequestMapField.OrderId] = GenerateOrderId();
            }

            //var evt = WorkContext.Resolve<IPaymentEventHandler>();

            // evt.Posting(requestModel);

            //兼容Money格式
            requestModel.Amount = ConvertAmount(requestModel.Amount);
            requestModel[RequestMapField.Amount] = requestModel.Amount.ToString(CultureInfo.InvariantCulture);
            requestModel[RequestMapField.Mid] = Settings.Mid;

            AttachRequest(requestModel, requestMapper);
            //合并映射到参数
            requestMapper.Combine(requestModel.MapValues);
            var request = requestMapper.GetMapResult();
            request[SignFieldName] = Sign(request);

            return CreateRequestContext(request, requestModel, GateWay);
        }



        protected virtual RequestContext CreateRequestContext(NameValueCollection request, PayRequestModel requestModel, string gateway)
        {
            return new RequestContext(request, this, requestModel) { GateWay = gateway };
        }


        protected abstract void AttachRequest(PayRequestModel model, IMap<RequestMapField> requestMapper);
        protected abstract string Sign(NameValueCollection requestParams);
        #endregion





        /* protected void DebugRequest(string typeTitle, HttpRequestBase httpRequest, LogLevel level = LogLevel.Debug)
         {
             var nv = httpRequest.HttpMethod == "POST" ? httpRequest.Form : httpRequest.QueryString;

             if (Logger.IsEnabled(level))
             {
                 if (httpRequest.HttpMethod == "POST")
                 {
                     using (StreamReader sr = new StreamReader(httpRequest.InputStream, Encoding))
                     {
                         var s = sr.ReadToEnd();
                         Logger.Information(@"{0} {1},StreamContent:{2}", Attribute.ProviderName, typeTitle, s);
                     }
                 }
                 else
                 {
                     Logger.Information(@"{0} {1},RawUrl:{2}", Attribute.ProviderName, typeTitle, httpRequest.RawUrl);
                 }
             }
         }*/
    }
}
