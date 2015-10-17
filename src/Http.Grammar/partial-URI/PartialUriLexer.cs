namespace Http.Grammar
{
    using System;

    using TextFx;

    public class PartialUriLexer : Lexer<PartialUri>
    {
        public override ReadResult<PartialUri> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}