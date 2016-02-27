namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        public ReservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Reserved> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Reserved>.FromResult(new Reserved(result.Element));
            }
            return ReadResult<Reserved>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}