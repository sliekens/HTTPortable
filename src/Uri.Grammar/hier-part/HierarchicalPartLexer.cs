namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class HierarchicalPartLexer : Lexer<HierarchicalPart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public HierarchicalPartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HierarchicalPart> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HierarchicalPart>.FromResult(new HierarchicalPart(result.Element));
            }
            return ReadResult<HierarchicalPart>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}