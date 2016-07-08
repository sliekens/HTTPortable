using Txt;
using Txt.Core;

namespace Http
{
    public interface IOptionalDelimitedListLexerFactory
    {
        ILexer<OptionalDelimitedList> Create(ILexer<Element> lexer);
    }
}