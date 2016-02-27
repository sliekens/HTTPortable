namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        public override ReadResult<ObsoleteText> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}