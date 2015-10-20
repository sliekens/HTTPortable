namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class MessageBodyLexer : Lexer<MessageBody>
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

        public override ReadResult<MessageBody> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<MessageBody>.FromError(
                        new SyntaxError
                            {
                                Message = "Expected 'message-body'.",
                                RuleName = "message-body",
                                Context = scanner.GetContext(),
                                InnerError = result.Error
                            });
            }

            var element = new MessageBody(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<MessageBody>.FromResult(element);
        }
    }
}