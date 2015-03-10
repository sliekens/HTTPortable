namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using SLANG;
    using ParameterPart = SLANG.Sequence<OptionalWhiteSpace, SLANG.Element, OptionalWhiteSpace, TransferParameter>;

    public class TransferExtension : Element
    {
        private readonly Token extension;
        private readonly IList<TransferParameter> parameters;

        public TransferExtension(Token extension, IList<ParameterPart> parameters, ITextContext context)
            : base(string.Concat(extension, string.Concat(parameters)), context)
        {
            Contract.Requires(extension != null);
            Contract.Requires(parameters != null);
            Contract.Requires(Contract.ForAll(parameters, sequence => sequence != null));
            Contract.Requires(context != null);
            this.extension = extension;
            this.parameters =
                new ReadOnlyCollection<TransferParameter>(parameters.Select(sequence => sequence.Element4).ToList());
        }

        public Token Extension
        {
            get
            {
                return this.extension;
            }
        }

        public IList<TransferParameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
    }
}