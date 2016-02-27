namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class AsteriskFormLexer : Lexer<AsteriskForm>
    {
        private readonly ILexer<Terminal> innerLexer;

        public AsteriskFormLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<AsteriskForm> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AsteriskForm>.FromResult(new AsteriskForm(result.Element));
            }
            return ReadResult<AsteriskForm>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
