namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathEmptyLexer : Lexer<PathEmpty>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">""</param>
        public PathEmptyLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PathEmpty element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PathEmpty(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PathEmpty);
            return false;
        }
    }
}