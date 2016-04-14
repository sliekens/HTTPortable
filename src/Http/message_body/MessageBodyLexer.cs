using System;
using Txt;
using Txt.ABNF;

namespace Http.message_body
{
    public sealed class MessageBodyLexer : Lexer<MessageBody>
    {
        private readonly ILexer<Repetition> innerLexer;

        public MessageBodyLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<MessageBody> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<MessageBody>.FromResult(new MessageBody(result.Element));
            }
            return ReadResult<MessageBody>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}