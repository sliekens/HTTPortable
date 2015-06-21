namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathEmptyLexer : Lexer<PathEmpty>
    {
        private readonly ILexer<TerminalString> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">""</param>
        public PathEmptyLexer(ILexer<TerminalString> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathEmpty element)
        {
            TerminalString result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new PathEmpty(result);
                return true;
            }

            element = default(PathEmpty);
            return false;
        }
    }
}