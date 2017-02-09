using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_vchar
{
    public sealed class FieldVisibleCharacterLexer : Lexer<FieldVisibleCharacter>
    {
        public FieldVisibleCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Alternation> InnerLexer { get; }

        protected override IEnumerable<FieldVisibleCharacter> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var alternation in InnerLexer.Read(scanner, context))
            {
                yield return new FieldVisibleCharacter(alternation);
            }
        }
    }
}
