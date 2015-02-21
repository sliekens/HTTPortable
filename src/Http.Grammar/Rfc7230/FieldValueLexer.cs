using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldValueLexer : Lexer<FieldValueToken>
    {
        private readonly ILexer<FieldContentToken> fieldContentLexer;
        private readonly ILexer<ObsFoldToken> obsFoldLexer;

        public FieldValueLexer()
            : this(new FieldContentLexer(), new ObsFoldLexer())
        {
        }

        public FieldValueLexer(ILexer<FieldContentToken> fieldContentLexer, ILexer<ObsFoldToken> obsFoldLexer)
        {
            Contract.Requires(fieldContentLexer != null);
            Contract.Requires(obsFoldLexer != null);
            this.fieldContentLexer = fieldContentLexer;
            this.obsFoldLexer = obsFoldLexer;
        }

        public override FieldValueToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            FieldValueToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-value'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldValueToken element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldValueToken);
                return false;
            }

            var context = scanner.GetContext();
            IList<Alternative<FieldContentToken, ObsFoldToken>> tokens = new List<Alternative<FieldContentToken, ObsFoldToken>>();
            for (; ; )
            {
                FieldContentToken fieldContent;
                if (this.fieldContentLexer.TryRead(scanner, out fieldContent))
                {
                    tokens.Add(new Alternative<FieldContentToken, ObsFoldToken>(fieldContent, context));
                }
                else
                {
                    ObsFoldToken obsFold;
                    if (this.obsFoldLexer.TryRead(scanner, out obsFold))
                    {
                        tokens.Add(new Alternative<FieldContentToken, ObsFoldToken>(obsFold, context));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            element = new FieldValueToken(tokens, context);
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
