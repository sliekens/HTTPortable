namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public partial class Via : ElementList3<Via.Sequence>
    {
        public Via(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Sequence element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Sequence>>>> element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }

    public partial class Via
    {
        public partial class Sequence : Sequence<ReceivedProtocol, RequiredWhiteSpace, ReceivedBy, Sequence.Option>
        {
            public Sequence(ReceivedProtocol element1, RequiredWhiteSpace element2, ReceivedBy element3, Option element4, ITextContext context)
                : base(element1, element2, element3, element4, context)
            {
            }
        }
    }

    public partial class Via
    {
        public partial class Sequence
        {
            public partial class Option : Option<Option.Sequence>
            {
                public Option(ITextContext context)
                    : base(context)
                {
                }

                public Option(Sequence element, ITextContext context)
                    : base(element, context)
                {
                }
            }
        }
    }

    public partial class Via
    {
        public partial class Sequence
        {
            public partial class Option
            {
                public class Sequence : Sequence<RequiredWhiteSpace, Comment>
                {
                    public Sequence(RequiredWhiteSpace element1, Comment element2, ITextContext context)
                        : base(element1, element2, context)
                    {
                    }
                }
            }
        }
    }
}
