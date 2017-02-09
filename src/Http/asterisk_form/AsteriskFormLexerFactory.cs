using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.asterisk_form
{
    public class AsteriskFormLexerFactory : RuleLexerFactory<AsteriskForm>
    {
        static AsteriskFormLexerFactory()
        {
            Default = new AsteriskFormLexerFactory();
        }

        [NotNull]
        public static AsteriskFormLexerFactory Default { get; }

        public override ILexer<AsteriskForm> Create()
        {
            var innerLexer = Terminal.Create(@"*", StringComparer.Ordinal);
            return new AsteriskFormLexer(innerLexer);
        }
    }
}
