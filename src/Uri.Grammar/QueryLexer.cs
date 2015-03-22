namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class QueryLexer : Lexer<Query>
    {
        private readonly ILexer<PathCharacter> pathCharacterLexer;

        public QueryLexer()
            : this(new PathCharacterLexer())
        {
        }

        public QueryLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base("query")
        {
            Contract.Requires(pathCharacterLexer != null);
            this.pathCharacterLexer = pathCharacterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Query element)
        {
            var elements = new List<Alternative<PathCharacter, Element>>();
            var context = scanner.GetContext();
            while (!scanner.EndOfInput)
            {
                var innerContext = scanner.GetContext();
                PathCharacter pathCharacter;
                if (this.pathCharacterLexer.TryRead(scanner, out pathCharacter))
                {
                    elements.Add(new Alternative<PathCharacter, Element>(pathCharacter, 1, innerContext));
                }
                else if (!scanner.EndOfInput && scanner.TryMatch('/'))
                {
                    var e = new Element('/', innerContext);
                    elements.Add(new Alternative<PathCharacter, Element>(e, 2, innerContext));
                }
                else if (!scanner.EndOfInput && scanner.TryMatch('?'))
                {
                    var e = new Element('?', innerContext);
                    elements.Add(new Alternative<PathCharacter, Element>(e, 2, innerContext));
                }
                else
                {
                    break;
                }

                context = scanner.GetContext();
            }

            element = new Query(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.pathCharacterLexer != null);
        }
    }
}