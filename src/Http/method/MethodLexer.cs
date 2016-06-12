using System;
using Http.token;
using Txt;
using Txt.Core;

namespace Http.method
{
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

        public override ReadResult<Method> ReadImpl(ITextScanner scanner)
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