namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;

    using Alternative = SLANG.Alternative<Unreserved, PercentEncoding, SubcomponentsDelimiter>;

    

    public class RegisteredNameLexer : Lexer<RegisteredName>
    {
        private readonly ILexer<Unreserved> unreservedLexer;

        private readonly ILexer<PercentEncoding> percentEncodingLexer;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        public RegisteredNameLexer()
            : this(new UnreservedLexer(), new PercentEncodingLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public RegisteredNameLexer(ILexer<Unreserved> unreservedLexer, ILexer<PercentEncoding> percentEncodingLexer, ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
            : base("reg-name")
        {
            Contract.Requires(unreservedLexer != null);
            Contract.Requires(percentEncodingLexer != null);
            Contract.Requires(subcomponentsDelimiterLexer != null);
            this.unreservedLexer = unreservedLexer;
            this.percentEncodingLexer = percentEncodingLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RegisteredName element)
        {
            var context = scanner.GetContext();
            var elements = new List<Alternative>();
            while (!scanner.EndOfInput)
            {
                var innerContext = scanner.GetContext();
                Alternative alternative;
                Unreserved unreserved;
                if (this.unreservedLexer.TryRead(scanner, out unreserved))
                {
                    alternative = new Alternative(unreserved, innerContext);
                }
                else
                {
                    PercentEncoding percentEncoding;
                    if (this.percentEncodingLexer.TryRead(scanner, out percentEncoding))
                    {
                        alternative = new Alternative(percentEncoding, innerContext);
                    }
                    else
                    {
                        SubcomponentsDelimiter subcomponentsDelimiter;
                        if (this.subcomponentsDelimiterLexer.TryRead(scanner, out subcomponentsDelimiter))
                        {
                            alternative = new Alternative(subcomponentsDelimiter, innerContext);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                elements.Add(alternative);
            }

            element = new RegisteredName(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.unreservedLexer != null);
            Contract.Invariant(this.percentEncodingLexer != null);
            Contract.Invariant(this.subcomponentsDelimiterLexer != null);
        }
    }
}