using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.Upgrade
{
    public sealed class UpgradeLexer : Lexer<Upgrade>
    {
        public UpgradeLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<RequiredDelimitedList> InnerLexer { get; }

        protected override IEnumerable<Upgrade> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var requiredDelimitedList in InnerLexer.Read(scanner, context))
            {
                yield return new Upgrade(requiredDelimitedList);
            }
        }
    }
}
