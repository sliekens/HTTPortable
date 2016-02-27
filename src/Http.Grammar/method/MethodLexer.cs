namespace Http.Grammar
{
    using System;

    using TextFx;

    public sealed class MethodLexer : Lexer<Method>
    {
        private readonly ILexer<Token> innerLexer;

        public MethodLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Method> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Method>.FromResult(new Method(result.Element));
            }
            return ReadResult<Method>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}