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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out StatusCode element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new StatusCode(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }
                
                return true;
            }

            element = default(StatusCode);
            return false;
        }
    }
}