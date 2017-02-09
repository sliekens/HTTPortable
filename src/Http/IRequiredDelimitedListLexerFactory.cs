using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http
{
    public interface IRequiredDelimitedListLexerFactory
    {
        [NotNull]
        ILexer<RequiredDelimitedList> Create([NotNull] ILexer<Element> lexer);
    }
}
