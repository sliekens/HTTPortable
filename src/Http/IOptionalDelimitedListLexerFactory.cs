using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http
{
    public interface IOptionalDelimitedListLexerFactory
    {
        [NotNull]
        ILexer<OptionalDelimitedList> Create([NotNull] ILexer<Element> lexer);
    }
}