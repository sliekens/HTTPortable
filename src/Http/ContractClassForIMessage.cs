namespace Http
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IMessage))]
    internal abstract class ContractClassForIMessage : IMessage
    {
        public IHeaderCollection Headers
        {
            get
            {
                Contract.Ensures(Contract.Result<IHeaderCollection>() != null);
                throw new System.NotImplementedException();
            }
        }
    }
}