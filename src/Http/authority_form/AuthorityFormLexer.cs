using System;
using Txt;
using Txt.Core;
using UriSyntax.authority;

namespace Http.authority_form
{
    public sealed class AuthorityFormLexer : Lexer<AuthorityForm>
    {
        private readonly ILexer<Authority> innerLexer;

        public AuthorityFormLexer(ILexer<Authority> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<AuthorityForm> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AuthorityForm>.FromResult(new AuthorityForm(result.Element));
            }
            return ReadResult<AuthorityForm>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
