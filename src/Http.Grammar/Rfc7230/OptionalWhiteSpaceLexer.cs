using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public OptionalWhiteSpaceLexer()
            : this(new WhiteSpaceLexer())
        {
        }

        public OptionalWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer)
        {
            Contract.Requires(whiteSpaceLexer != null);
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        public override OptionalWhiteSpace Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            OptionalWhiteSpace element;
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'OWS'");
        }

        public override bool TryRead(ITextScanner scanner, out OptionalWhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(OptionalWhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            WhiteSpace whiteSpace;
            IList<WhiteSpace> elements = new List<WhiteSpace>();
            while (this.whiteSpaceLexer.TryRead(scanner, out whiteSpace))
            {
                elements.Add(whiteSpace);
            }

            element = new OptionalWhiteSpace(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.whiteSpaceLexer != null);
        }
    }
}