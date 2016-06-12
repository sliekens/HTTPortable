using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.Host
{
    public sealed class HostLexer : Lexer<Host>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HostLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Host> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Host>.FromResult(new Host(result.Element));
            }
            return ReadResult<Host>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
