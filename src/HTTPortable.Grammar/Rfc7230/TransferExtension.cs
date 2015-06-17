namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using ParameterPart = SLANG.Sequence<OptionalWhiteSpace, SLANG.Element, OptionalWhiteSpace, TransferParameter>;

    public class TransferExtension : Sequence<Token, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TransferParameter>>>
    {
        public TransferExtension(Token element1, Repetition<ParameterPart> element2, ITextContext context)
            : base(element1, element2, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(context != null);
        }
    }
}