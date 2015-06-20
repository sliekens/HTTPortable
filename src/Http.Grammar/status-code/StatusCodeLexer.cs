namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class StatusCodeLexer : Lexer<StatusCode>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">3DIGIT</param>
        public StatusCodeLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out StatusCode element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new StatusCode(result);
                return true;
            }

            element = default(StatusCode);
            return false;
        }
    }
}