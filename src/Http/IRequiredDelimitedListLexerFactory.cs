using Txt;
using Txt.Core;

namespace Http
{
    public interface IRequiredDelimitedListLexerFactory
    {
        ILexer<RequiredDelimitedList> Create(ILexer<Element> lexer);
    }
}