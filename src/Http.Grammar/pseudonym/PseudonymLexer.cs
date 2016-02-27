namespace Http.Grammar
{
    using System;

    using TextFx;

    public sealed class PseudonymLexer : Lexer<Pseudonym>
    {
        private readonly ILexer<Token> innerLexer;

        public PseudonymLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Pseudonym> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Pseudonym>.FromResult(new Pseudonym(result.Element));
            }
            return ReadResult<Pseudonym>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}