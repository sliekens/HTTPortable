using System;
using Http.OWS;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.BWS
{
    public class BadWhiteSpaceLexerFactory : ILexerFactory<BadWhiteSpace>
    {
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        public BadWhiteSpaceLexerFactory([NotNull] ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer)
        {
            if (optionalWhiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexer));
            }
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
        }

        public ILexer<BadWhiteSpace> Create()
        {
            return new BadWhiteSpaceLexer(optionalWhiteSpaceLexer);
        }
    }
}
