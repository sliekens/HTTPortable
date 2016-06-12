using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public sealed class OptionalDelimitedListLexer : Lexer<OptionalDelimitedList>
    {
        private readonly ILexer<Repetition> innerLexer;

        public OptionalDelimitedListLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<OptionalDelimitedList> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<OptionalDelimitedList>.FromResult(new OptionalDelimitedList(result.Element));
            }
            return ReadResult<OptionalDelimitedList>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}