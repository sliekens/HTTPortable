namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolNameLexer : Lexer<ProtocolName>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ProtocolName element)
        {
            throw new NotImplementedException();
        }
    }
}