namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        public RequiredWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequiredWhiteSpace> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RequiredWhiteSpace>.FromResult(new RequiredWhiteSpace(result.Element));
            }
            return ReadResult<RequiredWhiteSpace>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}