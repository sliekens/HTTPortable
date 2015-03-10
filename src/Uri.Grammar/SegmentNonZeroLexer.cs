namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class SegmentNonZeroLexer : Lexer<SegmentNonZero>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        public SegmentNonZeroLexer()
            : this(new PathCharacterLexer())
        {
        }

        public SegmentNonZeroLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base("segment-nz")
        {
            Contract.Requires(pathCharacterLexer != null);
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out SegmentNonZero element)
        {
            var elements = new List<PathCharacter>();
            var context = scanner.GetContext();
            while (!scanner.EndOfInput)
            {
                PathCharacter pathCharacter;
                if (!this.pathCharacterLexer.TryRead(scanner, out pathCharacter))
                {
                    break;
                }

                elements.Add(pathCharacter);
            }

            if (elements.Count == 0)
            {
                element = default(SegmentNonZero);
                return false;
            }

            element = new SegmentNonZero(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathCharacterLexer != null);
        }
    }
}