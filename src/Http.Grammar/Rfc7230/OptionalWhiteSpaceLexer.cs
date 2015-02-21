using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly HorizontalTabLexer horizontalTabLexer;
        private readonly SpaceLexer SpaceLexer;

        public OptionalWhiteSpaceLexer()
            : this(new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        public OptionalWhiteSpaceLexer(SpaceLexer SpaceLexer, HorizontalTabLexer horizontalTabLexer)
        {
            Contract.Requires(SpaceLexer != null);
            Contract.Requires(horizontalTabLexer != null);
            this.SpaceLexer = SpaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        public override OptionalWhiteSpace Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            OptionalWhiteSpace token;
            if (TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'OWS'");
        }

        public override bool TryRead(ITextScanner scanner, out OptionalWhiteSpace token)
        {
            if (scanner.EndOfInput)
            {
                token = default(OptionalWhiteSpace);
                return false;
            }

            // TODO: refactoring using WSP lexers
            var context = scanner.GetContext();
            IList<WhiteSpace> elements = new List<WhiteSpace>();
            for (;;)
            {
                Space sp;
                if (SpaceLexer.TryRead(scanner, out sp))
                {
                    elements.Add(new WhiteSpace(sp, context));
                }
                else
                {
                    HorizontalTab hTab;
                    if (this.horizontalTabLexer.TryRead(scanner, out hTab))
                    {
                        elements.Add(new WhiteSpace(hTab, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            token = new OptionalWhiteSpace(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.SpaceLexer != null);
            Contract.Invariant(this.horizontalTabLexer != null);
        }
    }
}