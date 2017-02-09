using System;
using Http.comment;
using Http.received_by;
using Http.received_protocol;
using Http.RWS;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Via
{
    public sealed class ViaLexerFactory : RuleLexerFactory<Via>
    {
        static ViaLexerFactory()
        {
            Default = new ViaLexerFactory(
                Http.RequiredDelimitedListLexerFactory.Default,
                received_protocol.ReceivedProtocolLexerFactory.Default.Singleton(),
                RWS.RequiredWhiteSpaceLexerFactory.Default.Singleton(),
                received_by.ReceivedByLexerFactory.Default.Singleton(),
                comment.CommentLexerFactory.Default.Singleton());
        }

        public ViaLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexerFactory<ReceivedProtocol> receivedProtocolLexerFactory,
            [NotNull] ILexerFactory<RequiredWhiteSpace> requiredWhiteSpaceLexerFactory,
            [NotNull] ILexerFactory<ReceivedBy> receivedByLexerFactory,
            [NotNull] ILexerFactory<Comment> commentLexerFactory)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (receivedProtocolLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(receivedProtocolLexerFactory));
            }
            if (requiredWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredWhiteSpaceLexerFactory));
            }
            if (receivedByLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(receivedByLexerFactory));
            }
            if (commentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(commentLexerFactory));
            }
            RequiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            ReceivedProtocolLexerFactory = receivedProtocolLexerFactory;
            RequiredWhiteSpaceLexerFactory = requiredWhiteSpaceLexerFactory;
            ReceivedByLexerFactory = receivedByLexerFactory;
            CommentLexerFactory = commentLexerFactory;
        }

        [NotNull]
        public static ViaLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Comment> CommentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ReceivedBy> ReceivedByLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ReceivedProtocol> ReceivedProtocolLexerFactory { get; }

        [NotNull]
        public IRequiredDelimitedListLexerFactory RequiredDelimitedListLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RequiredWhiteSpace> RequiredWhiteSpaceLexerFactory { get; }

        public override ILexer<Via> Create()
        {
            var rws = RequiredWhiteSpaceLexerFactory.Create();
            var innerLexer = RequiredDelimitedListLexerFactory.Create(
                Concatenation.Create(
                    ReceivedProtocolLexerFactory.Create(),
                    rws,
                    ReceivedByLexerFactory.Create(),
                    Option.Create(
                        Concatenation.Create(
                            rws,
                            CommentLexerFactory.Create()))));
            return new ViaLexer(innerLexer);
        }
    }
}
