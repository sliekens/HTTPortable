namespace Uri.Grammar.unreserved
{
    using SLANG;

    public class UnreservedAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">ALPHA / DIGIT / "-" / "." / "_" / "~"</param>
        public UnreservedAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }

    public class Unreserved3StringLexer : StringLexer
    {
        public Unreserved3StringLexer()
            : base(@"-")
        {
        }
    }

    public class Unreserved4StringLexer : StringLexer
    {
        public Unreserved4StringLexer()
            : base(@".")
        {
        }
    }

    public class Unreserved5StringLexer : StringLexer
    {
        public Unreserved5StringLexer()
            : base(@"_")
        {
        }
    }

    public class Unreserved6StringLexer : StringLexer
    {
        public Unreserved6StringLexer()
            : base(@"~")
        {
        }
    }
}
