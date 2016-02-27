namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class QueryLexer : Lexer<Query>
    {
        private readonly ILexer<Repetition> innerLexer;

        public QueryLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Query> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Query>.FromResult(new Query(result.Element));
            }
            return ReadResult<Query>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}