namespace Http.Grammar
{
    using System;

    using TextFx;

    public class MessageBodyLexer : Lexer<MessageBody>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out MessageBody element)
        {
            throw new NotImplementedException();
        }
    }
}