using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<Space> SpaceLexer;
        private readonly ILexer<HorizontalTab> hTabLexer;

        public RequiredWhiteSpaceLexer()
            : this(new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        public RequiredWhiteSpaceLexer(ILexer<Space> SpaceLexer, ILexer<HorizontalTab> hTabLexer)
        {
            Contract.Requires(SpaceLexer != null);
            Contract.Requires(hTabLexer != null);
            this.SpaceLexer = SpaceLexer;
            this.hTabLexer = hTabLexer;
        }

        public override RequiredWhiteSpace Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            RequiredWhiteSpace element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'RWS'");
        }

        public override bool TryRead(ITextScanner scanner, out RequiredWhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RequiredWhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            Space space;
            HorizontalTab horizontalTab;
            IList<WhiteSpace> tokens = new List<WhiteSpace>();
            if (this.SpaceLexer.TryRead(scanner, out space))
            {
                tokens.Add(new WhiteSpace(space, context));
            }
            else
            {
                if (hTabLexer.TryRead(scanner, out horizontalTab))
                {
                    tokens.Add(new WhiteSpace(horizontalTab, context));
                }
                else
                {
                    element = default(RequiredWhiteSpace);
                    return false;
                }
            }


            // BUG: context is not updated
            for (; ; )
            {
                if (SpaceLexer.TryRead(scanner, out space))
                {
                    tokens.Add(new WhiteSpace(space, context));
                }
                else
                {
                    if (hTabLexer.TryRead(scanner, out horizontalTab))
                    {
                        tokens.Add(new WhiteSpace(horizontalTab, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            element = new RequiredWhiteSpace(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.SpaceLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }
    }
}
