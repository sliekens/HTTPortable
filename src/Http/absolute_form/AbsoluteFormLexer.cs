using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteFormLexer : Lexer<AbsoluteForm>
    {
        public AbsoluteFormLexer([NotNull] ILexer<AbsoluteUri> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<AbsoluteUri> InnerLexer { get; }

        protected override IEnumerable<AbsoluteForm> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var absoluteUri in InnerLexer.Read(scanner, context))
            {
                yield return new AbsoluteForm(absoluteUri);
            }
        }
    }
}