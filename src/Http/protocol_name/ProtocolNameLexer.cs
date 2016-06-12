using System;
using Http.token;
using Txt;
using Txt.Core;

namespace Http.protocol_name
{
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

        public override ReadResult<ProtocolName> ReadImpl(ITextScanner scanner)
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