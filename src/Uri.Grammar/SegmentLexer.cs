namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class SegmentLexer : Lexer<Segment>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        public SegmentLexer()
            : this(new PathCharacterLexer())
        {
        }

        public SegmentLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base("segment")
        {
            Contract.Requires(pathCharacterLexer != null);
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Segment element)
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

            element = new Segment(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathCharacterLexer != null);
        }
    }
}