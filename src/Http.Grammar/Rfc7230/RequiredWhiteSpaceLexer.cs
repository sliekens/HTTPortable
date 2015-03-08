using System.Collections.Generic;
using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public RequiredWhiteSpaceLexer()
            : this(new WhiteSpaceLexer())
        {
        }

        public RequiredWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer)
            : base("RWS")
        {
            Contract.Requires(whiteSpaceLexer != null);
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RequiredWhiteSpace element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RequiredWhiteSpace);
                return false;
            }

            var context = scanner.GetContext();
            WhiteSpace whiteSpace;
            IList<WhiteSpace> elements = new List<WhiteSpace>();
            while (this.whiteSpaceLexer.TryRead(scanner, out whiteSpace))
            {
                elements.Add(whiteSpace);
            }

            if (elements.Count == 0)
            {
                element = default(RequiredWhiteSpace);
                return false;
            }

            element = new RequiredWhiteSpace(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.whiteSpaceLexer != null);
        }
    }
}
