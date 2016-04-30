using System;
using Http.token;
using Txt;

namespace Http.connection_option
{
    public sealed class ConnectionOptionLexer : Lexer<ConnectionOption>
    {
        private readonly ILexer<Token> innerLexer;

        public ConnectionOptionLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ConnectionOption> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ConnectionOption>.FromResult(new ConnectionOption(result.Element));
            }
            return ReadResult<ConnectionOption>.FromSyntaxError(
                SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
