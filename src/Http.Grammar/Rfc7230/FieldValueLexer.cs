using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldValueLexer : Lexer<FieldValue>
    {
        private readonly ILexer<FieldContent> fieldContentLexer;
        private readonly ILexer<ObsFold> obsFoldLexer;

        public FieldValueLexer()
            : this(new FieldContentLexer(), new ObsFoldLexer())
        {
        }

        public FieldValueLexer(ILexer<FieldContent> fieldContentLexer, ILexer<ObsFold> obsFoldLexer)
        {
            Contract.Requires(fieldContentLexer != null);
            Contract.Requires(obsFoldLexer != null);
            this.fieldContentLexer = fieldContentLexer;
            this.obsFoldLexer = obsFoldLexer;
        }

        public override FieldValue Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            FieldValue element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-value'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldValue element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldValue);
                return false;
            }

            var context = scanner.GetContext();
            IList<Alternative<FieldContent, ObsFold>> tokens = new List<Alternative<FieldContent, ObsFold>>();
            for (; ; )
            {
                FieldContent fieldContent;
                if (this.fieldContentLexer.TryRead(scanner, out fieldContent))
                {
                    tokens.Add(new Alternative<FieldContent, ObsFold>(fieldContent, context));
                }
                else
                {
                    ObsFold obsFold;
                    if (this.obsFoldLexer.TryRead(scanner, out obsFold))
                    {
                        tokens.Add(new Alternative<FieldContent, ObsFold>(obsFold, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            element = new FieldValue(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldContentLexer != null);
            Contract.Invariant(this.obsFoldLexer != null);
        }

    }
}
