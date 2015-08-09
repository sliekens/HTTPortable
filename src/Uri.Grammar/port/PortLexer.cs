namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PortLexer : Lexer<Port>
    {
        private readonly ILexer<Repetition> innerLexer;

        public PortLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Port element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Port(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Port);
            return false;
        }
    }
}