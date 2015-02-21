using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ObsFoldLexer : Lexer<ObsFoldToken>
    {
        private readonly ILexer<EndOfLine> crLfLexer;
        private readonly ILexer<Space> spLexer;
        private readonly ILexer<HorizontalTab> hTabLexer;

        public ObsFoldLexer()
            : this(new EndOfLineLexer(), new SpaceLexer(), new HorizontalTabLexer())
        {
        }

        public ObsFoldLexer(ILexer<EndOfLine> crLfLexer, ILexer<Space> spLexer, ILexer<HorizontalTab> hTabLexer)
        {
            Contract.Requires(crLfLexer != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.crLfLexer = crLfLexer;
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override ObsFoldToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            ObsFoldToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'obs-fold'");
        }

        public override bool TryRead(ITextScanner scanner, out ObsFoldToken element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ObsFoldToken);
                return false;
            }

            var context = scanner.GetContext();
            EndOfLine endOfLine;
            if (!this.crLfLexer.TryRead(scanner, out endOfLine))
            {
                element = default(ObsFoldToken);
                return false;
            }

            Space space;
            HorizontalTab horizontalTab;
            IList<WhiteSpace> elements = new List<WhiteSpace>();
            if (this.spLexer.TryRead(scanner, out space))
            {
                elements.Add(new WhiteSpace(space, context));
            }
            else
            {
                if (hTabLexer.TryRead(scanner, out horizontalTab))
                {
                    elements.Add(new WhiteSpace(horizontalTab, context));
                }
                else
                {
                    this.crLfLexer.PutBack(scanner, endOfLine);
                    element = default(ObsFoldToken);
                    return false;
                }
            }

            // TODO: refactor using OWS lexers
            for (; ; )
            {
                if (spLexer.TryRead(scanner, out space))
                {
                    elements.Add(new WhiteSpace(space, context));
                }
                else
                {
                    if (hTabLexer.TryRead(scanner, out horizontalTab))
                    {
                        elements.Add(new WhiteSpace(horizontalTab, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            element = new ObsFoldToken(endOfLine, elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crLfLexer != null);
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }

    }
}
