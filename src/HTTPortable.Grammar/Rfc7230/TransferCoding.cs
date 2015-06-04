﻿namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class TransferCoding : Alternative<Element, TransferExtension>
    {
        public TransferCoding(Element element, ITextContext context)
            : base(element, 1, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public TransferCoding(TransferExtension element, ITextContext context)
            : base(element, 2, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}