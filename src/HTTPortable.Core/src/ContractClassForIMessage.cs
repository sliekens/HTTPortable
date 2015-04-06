namespace Http
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IMessage))]
    internal abstract class ContractClassForIMessage : IMessage
    {
        public IHeaderCollection Headers
        {
            get
            {
                Contract.Ensures(Contract.Result<IHeaderCollection>() != null);
                throw new NotImplementedException();
            }
        }

        public Version HttpVersion
        {
            get
            {
                Contract.Ensures(Contract.Result<Version>() != null);
                throw new NotImplementedException();
            }
        }
    }
}