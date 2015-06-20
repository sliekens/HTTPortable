namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolVersionLexer : Lexer<ProtocolVersion>
    {
        public override bool TryRead(ITextScanner scanner, out ProtocolVersion element)
        {
            throw new NotImplementedException();
        }
    }
}