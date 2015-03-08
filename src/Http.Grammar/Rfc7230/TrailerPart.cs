namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;



    public class TrailerPart : Element
    {
        private readonly IList<HeaderField> headerFields;

        public TrailerPart(IList<HeaderField> headerFields, EndOfLine endOfLine, ITextContext context)
            : base(string.Concat(string.Concat(headerFields), endOfLine), context)
        {
            Contract.Requires(headerFields != null);
            Contract.Requires(headerFields.Count != 0);
            Contract.Requires(Contract.ForAll(headerFields, field => field != null));
            Contract.Requires(endOfLine != null);
            Contract.Requires(context != null);
            this.headerFields = headerFields;
        }

        public IList<HeaderField> HeaderFields
        {
            get
            {
                return new ReadOnlyCollection<HeaderField>(this.headerFields);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headerFields != null);
        }
    }
}