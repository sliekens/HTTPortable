using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.segment;

namespace Http.absolute_path
{
    public class AbsolutePathLexerFactory : RuleLexerFactory<AbsolutePath>
    {
        static AbsolutePathLexerFactory()
        {
            Default = new AbsolutePathLexerFactory(SegmentLexerFactory.Default.Singleton());
        }

        public AbsolutePathLexerFactory([NotNull] ILexerFactory<Segment> segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            Segment = segment;
        }

        [NotNull]
        public static AbsolutePathLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Segment> Segment { get; set; }

        public override ILexer<AbsolutePath> Create()
        {
            var innerLexer =
                Repetition.Create(
                    Concatenation.Create(
                        Terminal.Create(@"/", StringComparer.Ordinal),
                        Segment.Create()),
                    1,
                    int.MaxValue);
            return new AbsolutePathLexer(innerLexer);
        }
    }
}
