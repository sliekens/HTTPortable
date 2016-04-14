using System;
using Txt;
using Uri.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteFormLexer : Lexer<AbsoluteForm>
    {
        private readonly ILexer<AbsoluteUri> innerLexer;

        public AbsoluteFormLexer(ILexer<AbsoluteUri> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsoluteForm> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AbsoluteForm>.FromResult(new AbsoluteForm(result.Element));
            }
            return ReadResult<AbsoluteForm>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}