namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        private readonly ILexer<Alternative> innerLexer;

        public GenericDelimiterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<GenericDelimiter> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<GenericDelimiter>.FromResult(new GenericDelimiter(result.Element));
            }
            return ReadResult<GenericDelimiter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}