namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        public UnreservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Unreserved> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Unreserved>.FromResult(new Unreserved(result.Element));
            }
            return ReadResult<Unreserved>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}