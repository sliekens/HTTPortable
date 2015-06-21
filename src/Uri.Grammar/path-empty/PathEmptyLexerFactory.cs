namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathEmptyLexerFactory : ILexerFactory<PathEmpty>
    {
        private readonly IStringLexerFactory stringLexerFactory;

        public PathEmptyLexerFactory(IStringLexerFactory stringLexerFactory)
        {
            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<PathEmpty> Create()
        {
            var innerLexer = this.stringLexerFactory.Create(string.Empty);
            return new PathEmptyLexer(innerLexer);
        }
    }
}