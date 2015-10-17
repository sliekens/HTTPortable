namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredDelimitedListLexer : Lexer<RequiredDelimitedList>
    {
        private readonly ILexer<Sequence> innerLexer;

        public RequiredDelimitedListLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequiredDelimitedList> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<RequiredDelimitedList>.FromError(new SyntaxError
                {
                    Message = "One or more syntax errors were found.",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new RequiredDelimitedList(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<RequiredDelimitedList>.FromResult(element);
        }
    }
}