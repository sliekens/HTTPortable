using System;
using Txt;
using Txt.ABNF;

namespace Http.start_line
{
    public sealed class StartLineLexer : Lexer<StartLine>
    {
        private readonly ILexer<Alternation> innerLexer;

        public StartLineLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<StartLine> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<StartLine>.FromResult(new StartLine(result.Element));
            }
            return ReadResult<StartLine>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}