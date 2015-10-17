namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalDelimitedListLexer : Lexer<OptionalDelimitedList>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">[ ( "," / element ) *( OWS "," [ OWS element ] ) ]</param>
        public OptionalDelimitedListLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<OptionalDelimitedList> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<OptionalDelimitedList>.FromError(new SyntaxError
                {
                    Message = "One or more syntax errors were found.",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new OptionalDelimitedList(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<OptionalDelimitedList>.FromResult(element);
        }
    }
}