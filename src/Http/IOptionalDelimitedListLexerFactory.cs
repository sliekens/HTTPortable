using Txt;

namespace Http
{
    public interface IOptionalDelimitedListLexerFactory
    {
        ILexer<OptionalDelimitedList> Create(ILexer lexer);
    }
}