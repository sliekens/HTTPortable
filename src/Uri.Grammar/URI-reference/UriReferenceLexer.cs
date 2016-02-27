namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class UriReferenceLexer : Lexer<UriReference>
    {
        private readonly ILexer<Alternative> innerLexer;

        public UriReferenceLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UriReference> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<UriReference>.FromResult(new UriReference(result.Element));
            }
            return ReadResult<UriReference>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}