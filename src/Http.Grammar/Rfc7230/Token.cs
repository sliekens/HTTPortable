namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class Token : Element
    {
        public Token(IList<TokenCharacter> tokenCharacters, ITextContext context)
            : base(string.Concat(tokenCharacters), context)
        {
            Contract.Requires(tokenCharacters != null);
            Contract.Requires(tokenCharacters.Count != 0);
            Contract.Requires(Contract.ForAll(tokenCharacters, c => c != null));
            Contract.Requires(context != null);
        }
    }
}