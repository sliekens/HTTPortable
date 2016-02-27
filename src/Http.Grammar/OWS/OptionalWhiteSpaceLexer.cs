namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        public OptionalWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<OptionalWhiteSpace> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<OptionalWhiteSpace>.FromResult(new OptionalWhiteSpace(result.Element));
            }
            return ReadResult<OptionalWhiteSpace>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}