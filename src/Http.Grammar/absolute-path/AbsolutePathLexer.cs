namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class AbsolutePathLexer : Lexer<AbsolutePath>
    {
        private readonly ILexer<Repetition> innerLexer;

        public AbsolutePathLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsolutePath> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AbsolutePath>.FromResult(new AbsolutePath(result.Element));
            }
            return ReadResult<AbsolutePath>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}