namespace Http.Grammar
{
    using TextFx;

    public interface IOptionalDelimitedListLexerFactory
    {
        ILexer<OptionalDelimitedList> Create(ILexer lexer);
    }
}