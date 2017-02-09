using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.obs_text
{
    public class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        public ObsoleteTextLexer([NotNull] ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Terminal> InnerLexer { get; }

        protected override IEnumerable<ObsoleteText> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new ObsoleteText(terminal);
            }
        }
    }
}
