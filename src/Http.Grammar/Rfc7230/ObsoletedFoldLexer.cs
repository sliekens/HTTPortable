using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class ObsoletedFoldLexer : Lexer<ObsoletedFold>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;

        private readonly ILexer<RequiredWhiteSpace> requiredWhiteSpaceLexer;

        public ObsoletedFoldLexer()
            : this(new EndOfLineLexer(), new RequiredWhiteSpaceLexer())
        {
        }

        public ObsoletedFoldLexer(ILexer<EndOfLine> endOfLineLexer, ILexer<RequiredWhiteSpace> requiredWhiteSpaceLexer)
            : base("obs-fold")
        {
            Contract.Requires(endOfLineLexer != null);
            Contract.Requires(requiredWhiteSpaceLexer != null);
            this.endOfLineLexer = endOfLineLexer;
            this.requiredWhiteSpaceLexer = requiredWhiteSpaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ObsoletedFold element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ObsoletedFold);
                return false;
            }

            var context = scanner.GetContext();
            EndOfLine endOfLine;
            if (!this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                element = default(ObsoletedFold);
                return false;
            }

            RequiredWhiteSpace requiredWhiteSpace;
            if (!this.requiredWhiteSpaceLexer.TryRead(scanner, out requiredWhiteSpace))
            {
                scanner.PutBack(endOfLine.Data);
                element = default(ObsoletedFold);
                return false;
            }

            element = new ObsoletedFold(endOfLine, requiredWhiteSpace, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.endOfLineLexer != null);
            Contract.Invariant(this.requiredWhiteSpaceLexer != null);
        }
    }
}
