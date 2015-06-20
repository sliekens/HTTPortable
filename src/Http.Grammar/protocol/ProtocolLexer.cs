namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolLexer : Lexer<Protocol>
    {
        public override bool TryRead(ITextScanner scanner, out Protocol element)
        {
            throw new NotImplementedException();
        }
    }
}