using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.OWS
{
    public sealed class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        public OptionalWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<OptionalWhiteSpace> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<OptionalWhiteSpace>.FromResult(new OptionalWhiteSpace(result.Element));
            }
            return ReadResult<OptionalWhiteSpace>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}