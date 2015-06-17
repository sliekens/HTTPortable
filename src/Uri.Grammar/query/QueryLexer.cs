namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class QueryLexer : Lexer<Query>
    {
        private readonly ILexer<Repetition> queryRepetitionLexer;

        public QueryLexer(ILexer<Repetition> queryRepetitionLexer)
        {
            if (queryRepetitionLexer == null)
            {
                throw new ArgumentNullException("queryRepetitionLexer", "Precondition: queryRepetitionLexer != null");
            }

            this.queryRepetitionLexer = queryRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Query element)
        {
            Repetition result;
            if (this.queryRepetitionLexer.TryRead(scanner, out result))
            {
                element = new Query(result);
                return true;
            }

            element = default(Query);
            return false;
        }
    }
}