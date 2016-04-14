using Txt;

namespace Http
{
    public interface IRequiredDelimitedListLexerFactory
    {
        ILexer<RequiredDelimitedList> Create(ILexer lexer);
    }
}