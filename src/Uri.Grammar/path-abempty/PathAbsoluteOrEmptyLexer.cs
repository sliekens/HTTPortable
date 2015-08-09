namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteOrEmptyLexer : Lexer<PathAbsoluteOrEmpty>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">*( "/" segment )</param>
        public PathAbsoluteOrEmptyLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PathAbsoluteOrEmpty element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PathAbsoluteOrEmpty(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PathAbsoluteOrEmpty);
            return false;
        }
    }
}