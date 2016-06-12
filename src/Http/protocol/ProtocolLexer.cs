using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol
{
    public sealed class ProtocolLexer : Lexer<Protocol>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ProtocolLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Protocol> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Protocol>.FromResult(new Protocol(result.Element));
            }
            return ReadResult<Protocol>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
