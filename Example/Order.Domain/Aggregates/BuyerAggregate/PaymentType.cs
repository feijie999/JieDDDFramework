using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Domain;

namespace Order.Domain.Aggregates.BuyerAggregate
{
    public class PaymentType
        : Enumeration
    {
        public static PaymentType Alipay = new AlipayType();
        public static PaymentType WeChat = new WeChatType();

        public PaymentType(int id, string name)
            : base(id, name)
        {
        }

        private class AlipayType : PaymentType
        {
            public AlipayType() : base(1, "Alipay")
            { }
        }

        private class WeChatType : PaymentType
        {
            public WeChatType() : base(2, "WeChat")
            { }
        }
    }
}
