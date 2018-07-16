using System;
using System.Collections.Generic;
using System.Text;
using FairyPay.PaymentProviders.Models;

namespace FairyPay.PaymentProviders.Context
{
    public class ReceiveContext
    {
        public Meta Meta { get; internal set; }
        public PayReceiveModel Model { get; internal set; }
        //public PaymentRecord Record { get; internal set; }
        public bool IsServerNotify { get; internal set; }

        public void SetResult(ResultType result)
        {
            Model.Result = result;
        }

        public ReceiveContext()
            : this(new PayReceiveModel())
        {
        }

        public ReceiveContext(PayReceiveModel model)
        {
            Model = model;
        }
    }
}
