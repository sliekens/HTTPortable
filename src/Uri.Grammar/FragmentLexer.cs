namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        public FragmentLexer()
            : this(new PathCharacterLexer())
        {
        }

        public FragmentLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base("fragment")
        {
            Contract.Requires(pathCharacterLexer != null);
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Fragment element)
        {
            var elements = new List<Alternative<PathCharacter, Element>>();
            var context = scanner.GetContext();
            element = new Fragment(elements, context);
            do
            {
                PathCharacter pathCharacter;
                if (this.pathCharacterLexer.TryRead(scanner, out pathCharacter))
                {
                    elements.Add(new Alternative<PathCharacter, Element>(pathCharacter, context));
                }
                else if (scanner.TryMatch('/'))
                {
                    var e = new Element('/', context);
                    elements.Add(new Alternative<PathCharacter, Element>(e, context));
                }
                else if (scanner.TryMatch('?'))
                {
                    var e = new Element('?', context);
                    elements.Add(new Alternative<PathCharacter, Element>(e, context));
                }
                else
                {
                    break;
                }

                context = scanner.GetContext();
            }
            while (!scanner.EndOfInput);

            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathCharacterLexer != null);
        }
    }
}
