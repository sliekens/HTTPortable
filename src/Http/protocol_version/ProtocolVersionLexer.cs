using System;
using Http.token;
using Txt;
using Txt.Core;

namespace Http.protocol_version
{
    public sealed class ProtocolVersionLexer : Lexer<ProtocolVersion>
    {
        private readonly ILexer<Token> innerLexer;

        public ProtocolVersionLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ProtocolVersion> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ProtocolVersion>.FromResult(new ProtocolVersion(result.Element));
            }
            return ReadResult<ProtocolVersion>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}