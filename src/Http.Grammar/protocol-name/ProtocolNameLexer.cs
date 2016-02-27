namespace Http.Grammar
{
    using System;

    using TextFx;

    public sealed class ProtocolNameLexer : Lexer<ProtocolName>
    {
        private readonly ILexer<Token> innerLexer;

        public ProtocolNameLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ProtocolName> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ProtocolName>.FromResult(new ProtocolName(result.Element));
            }
            return ReadResult<ProtocolName>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}