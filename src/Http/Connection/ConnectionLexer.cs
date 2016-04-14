using System;
using Txt;

namespace Http.Connection
{
    public sealed class ConnectionLexer : Lexer<Connection>
    {
        private readonly ILexer<RequiredDelimitedList> innerLexer;

        public ConnectionLexer(ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Connection> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Connection>.FromResult(new Connection(result.Element));
            }
            return ReadResult<Connection>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}