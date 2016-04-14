using System;
using Txt;
using Txt.ABNF;

namespace Http
{
    public sealed class RequiredDelimitedListLexer : Lexer<RequiredDelimitedList>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public RequiredDelimitedListLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequiredDelimitedList> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RequiredDelimitedList>.FromResult(new RequiredDelimitedList(result.Element));
            }
            return ReadResult<RequiredDelimitedList>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}