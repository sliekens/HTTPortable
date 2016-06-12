using System;
using Http.ctext;
using Http.quoted_pair;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.comment
{
    public class CommentLexerFactory : ILexerFactory<Comment>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<CommentText> commentTextLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<QuotedPair> quotedPairLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public CommentLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<CommentText> commentTextLexer,
            [NotNull] ILexer<QuotedPair> quotedPairLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (commentTextLexer == null)
            {
                throw new ArgumentNullException(nameof(commentTextLexer));
            }
            if (quotedPairLexer == null)
            {
                throw new ArgumentNullException(nameof(quotedPairLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.commentTextLexer = commentTextLexer;
            this.quotedPairLexer = quotedPairLexer;
        }

        public ILexer<Comment> Create()
        {
            var selfLexer = new ProxyLexer<Comment>();
            var commentLexer =
                new CommentLexer(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"(", StringComparer.Ordinal),
                        repetitionLexerFactory.Create(
                            alternationLexerFactory.Create(commentTextLexer, quotedPairLexer, selfLexer),
                            0,
                            int.MaxValue),
                        terminalLexerFactory.Create(@")", StringComparer.Ordinal)));
            selfLexer.Initialize(commentLexer);
            return commentLexer;
        }
    }
}
