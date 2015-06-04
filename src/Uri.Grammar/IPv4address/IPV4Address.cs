namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class IPv4Address : Element
    {
        private readonly DecimalOctet octet1;
        private readonly DecimalOctet octet2;
        private readonly DecimalOctet octet3;
        private readonly DecimalOctet octet4;

        public IPv4Address(DecimalOctet octet1, DecimalOctet octet2, DecimalOctet octet3, DecimalOctet octet4, 
            ITextContext context)
            : base(string.Concat(octet1.Data, ".", octet2.Data, ".", octet3.Data, ".", octet4.Data), context)
        {
            Contract.Requires(octet1 != null);
            Contract.Requires(octet2 != null);
            Contract.Requires(octet3 != null);
            Contract.Requires(octet4 != null);
            this.octet1 = octet1;
            this.octet2 = octet2;
            this.octet3 = octet3;
            this.octet4 = octet4;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.octet1 != null);
            Contract.Invariant(this.octet2 != null);
            Contract.Invariant(this.octet3 != null);
            Contract.Invariant(this.octet4 != null);
        }
    }
}