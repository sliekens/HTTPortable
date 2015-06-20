namespace Http.Grammar
{
    using TextFx;

    public interface IRequiredDelimitedListLexerFactory
    {
        ILexer<RequiredDelimitedList> Create(ILexer lexer);
    }
}