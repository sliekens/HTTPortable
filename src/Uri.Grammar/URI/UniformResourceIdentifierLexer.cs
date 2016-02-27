namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class UniformResourceIdentifierLexer : Lexer<UniformResourceIdentifier>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public UniformResourceIdentifierLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UniformResourceIdentifier> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<UniformResourceIdentifier>.FromResult(new UniformResourceIdentifier(result.Element));
            }
            return ReadResult<UniformResourceIdentifier>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}