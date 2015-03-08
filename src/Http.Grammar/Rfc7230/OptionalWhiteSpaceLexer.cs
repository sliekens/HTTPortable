using System.Collections.Generic;
using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public OptionalWhiteSpaceLexer()
            : this(new WhiteSpaceLexer())
        {
        }

        public OptionalWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer)
            : base("OWS")
        {
            Contract.Requires(whiteSpaceLexer != null);
            this.whiteSpaceLexer = whiteSpaceLexer;
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