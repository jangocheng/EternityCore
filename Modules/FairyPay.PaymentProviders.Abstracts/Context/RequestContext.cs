using System.Collections.Specialized;
using FairyPay.PaymentProviders.Models;
using FairyPay.PaymentProviders.Provider;

namespace FairyPay.PaymentProviders.Context
{
    public class RequestContext
    {
        //public PaymentProviderAttribute ProviderAttribute { get; internal set; }
        public NameValueCollection RequestParameters { get; }
        public ProviderSettings PaymentParameters { get { return ProviderInstance.Settings; } }

        public string GateWay { get; internal set; }

        //public string HttpMethod { get; internal set; }

        public PaymentServicesProviderBase ProviderInstance { get; }
        public PayRequestModel Model { get; }

        public string ErrorMessage { get; internal set; }


        internal RequestContext(NameValueCollection requestParameters, PaymentServicesProviderBase provider, PayRequestModel model)
        {
            ProviderInstance = provider;
            Model = model;
            RequestParameters = requestParameters;
        }
    }
}
