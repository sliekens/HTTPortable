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

        public override bool TryRead(ITextScanner scanner, out OptionalDelimitedList element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new OptionalDelimitedList(result);
                return true;
            }

            element = default(OptionalDelimitedList);
            return false;
        }
    }
}