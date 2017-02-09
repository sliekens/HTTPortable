using System;
using Http.ctext;
using Http.quoted_pair;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.comment
{
    public sealed class CommentLexerFactory : RuleLexerFactory<Comment>
    {
        static CommentLexerFactory()
        {
            Default = new CommentLexerFactory(
                ctext.CommentTextLexerFactory.Default.Singleton(),
                quoted_pair.QuotedPairLexerFactory.Default.Singleton());
        }

        public CommentLexerFactory(
            [NotNull] ILexerFactory<CommentText> commentTextLexerFactory,
            [NotNull] ILexerFactory<QuotedPair> quotedPairLexerFactory)
        {
            if (commentTextLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(commentTextLexerFactory));
            }
            if (quotedPairLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(quotedPairLexerFactory));
            }
            CommentTextLexerFactory = commentTextLexerFactory;
            QuotedPairLexerFactory = quotedPairLexerFactory;
        }

        [NotNull]
        public static CommentLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<CommentText> CommentTextLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<QuotedPair> QuotedPairLexerFactory { get; set; }

        public override ILexer<Comment> Create()
        {
            var selfLexer = new ProxyLexer<Comment>();
            var innerLexer = Concatenation.Create(
                Terminal.Create(@"(", StringComparer.Ordinal),
                Repetition.Create(
                    Alternation.Create(
                        CommentTextLexerFactory.Create(),
                        QuotedPairLexerFactory.Create(),
                        selfLexer),
                    0,
                    int.MaxValue),
                Terminal.Create(@")", StringComparer.Ordinal));
            var commentLexer = new CommentLexer(innerLexer);
            selfLexer.Initialize(commentLexer);
            return commentLexer;
        }
    }
}
