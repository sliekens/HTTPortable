namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathEmptyLexerFactory : ILexerFactory<PathEmpty>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PathEmptyLexerFactory(ITerminalLexerFactory terminalLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            this.terminalLexerFactory = terminalLexerFactory;
        }

        public ILexer<PathEmpty> Create()
        {
            var innerLexer = this.terminalLexerFactory.Create(string.Empty, StringComparer.Ordinal);
            return new PathEmptyLexer(innerLexer);
        }
    }
}