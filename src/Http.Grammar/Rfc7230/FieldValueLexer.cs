using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class FieldValueLexer : Lexer<FieldValue>
    {
        private readonly ILexer<FieldContent> fieldContentLexer;
        private readonly ILexer<ObsoletedFold> obsFoldLexer;

        public FieldValueLexer()
            : this(new FieldContentLexer(), new ObsoletedFoldLexer())
        {
        }

        public FieldValueLexer(ILexer<FieldContent> fieldContentLexer, ILexer<ObsoletedFold> obsFoldLexer)
            : base("field-value")
        {
            Contract.Requires(fieldContentLexer != null);
            Contract.Requires(obsFoldLexer != null);
            this.fieldContentLexer = fieldContentLexer;
            this.obsFoldLexer = obsFoldLexer;
        }

        public override bool TryRead(ITextScanner scanner, out FieldValue element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldValue);
                return false;
            }

            var context = scanner.GetContext();
            IList<Alternative<FieldContent, ObsoletedFold>> elements = new List<Alternative<FieldContent, ObsoletedFold>>();
            for (; ; )
            {
                FieldContent fieldContent;
                if (this.fieldContentLexer.TryRead(scanner, out fieldContent))
                {
                    elements.Add(new Alternative<FieldContent, ObsoletedFold>(fieldContent, context));
                }
                else
                {
                    ObsoletedFold obsoletedFold;
                    if (this.obsFoldLexer.TryRead(scanner, out obsoletedFold))
                    {
                        elements.Add(new Alternative<FieldContent, ObsoletedFold>(obsoletedFold, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            element = new FieldValue(elements, context);
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
