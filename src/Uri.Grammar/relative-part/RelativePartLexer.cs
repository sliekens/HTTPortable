namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class RelativePartLexer : Lexer<RelativePart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public RelativePartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RelativePart> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RelativePart>.FromResult(new RelativePart(result.Element));
            }
            return ReadResult<RelativePart>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}