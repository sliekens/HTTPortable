using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_protocol
{
    public sealed class ReceivedProtocolLexer : Lexer<ReceivedProtocol>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ReceivedProtocolLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ReceivedProtocol> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ReceivedProtocol>.FromResult(new ReceivedProtocol(result.Element));
            }
            return ReadResult<ReceivedProtocol>.FromSyntaxError(
                SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
