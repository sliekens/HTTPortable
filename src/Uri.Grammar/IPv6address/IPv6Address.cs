namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using TextFx.ABNF;

    public class IPv6Address : Alternative
    {
        public IPv6Address(Alternative alternative)
            : base(alternative)
        {
        }

        public override string GetWellFormedText()
        {
            return ConvertToIPv6Address(this.GetBytes());
        }

        private delegate byte[] BytesFactory();
        
        public byte[] GetBytes()
        {
            var sequence = (Sequence)this.Element;
            switch (this.Ordinal)
            {
                case 1:
                    return GetBytes1(sequence);
                case 2:
                    return GetBytes2(sequence);
                case 3:
                    return GetBytes3(sequence);
                case 4:
                    return GetBytes4(sequence);
                case 5:
                    return GetBytes5(sequence);
                case 6:
                    return GetBytes6(sequence);
                case 7:
                    return GetBytes7(sequence);
                case 8:
                    return GetBytes8(sequence);
                case 9:
                    return GetBytes9(sequence);
            }

            return null;
        }

        private static byte[] GetBytes1(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 6; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.RightAlign.Add(h16.GetBytes);
            }

            var ls32 = (LeastSignificantInt32)sequence.Elements[1];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes2(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var rep = (Repetition)sequence.Elements[1];
            for (var i = 0; i < 5; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.RightAlign.Add(h16.GetBytes);
            }

            var ls32 = (LeastSignificantInt32)sequence.Elements[2];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes3(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                var h16 = (HexadecimalInt16)opt1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var rep = (Repetition)sequence.Elements[2];
            for (var i = 0; i < 4; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.RightAlign.Add(h16.GetBytes);
            }
            var ls32 = (LeastSignificantInt32)sequence.Elements[3];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes4(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt2((Alternative)opt1.Elements[0], ctx);
            }

            var rep = (Repetition)sequence.Elements[2];
            for (var i = 0; i < 3; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.RightAlign.Add(h16.GetBytes);
            }

            var ls32 = (LeastSignificantInt32)sequence.Elements[3];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes5(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt3((Alternative)opt1.Elements[0], ctx);
            }

            var rep = (Repetition)sequence.Elements[2];
            for (var i = 0; i < 2; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.RightAlign.Add(h16.GetBytes);
            }

            var ls32 = (LeastSignificantInt32)sequence.Elements[3];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes6(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt4((Alternative)opt1.Elements[0], ctx);
            }

            var h16 = (HexadecimalInt16)sequence.Elements[2];
            ctx.RightAlign.Add(h16.GetBytes);

            var ls32 = (LeastSignificantInt32)sequence.Elements[4];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes7(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt5((Alternative)opt1.Elements[0], ctx);
            }

            var ls32 = (LeastSignificantInt32)sequence.Elements[2];
            ctx.RightAlign.Add(ls32.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes8(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt6((Alternative)opt1.Elements[0], ctx);
            }

            var h16 = (HexadecimalInt16)sequence.Elements[2];
            ctx.RightAlign.Add(h16.GetBytes);

            return ctx.GetResult();
        }

        private static byte[] GetBytes9(Sequence sequence)
        {
            var ctx = new BytesFactoryContext();
            var opt1 = (Repetition)sequence.Elements[0];
            if (opt1.Elements.Count != 0)
            {
                GetBytesh16Alt7((Alternative)opt1.Elements[0], ctx);
            }

            return ctx.GetResult();
        }

        private static void GetBytesh16Alt2(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                ctx.LeftAlign.Add(((HexadecimalInt16)alternative.Element).GetBytes);
                return;
            }

            var seq = (Sequence)alternative.Element;
            ctx.LeftAlign.Add(((HexadecimalInt16)seq.Elements[0]).GetBytes);
            ctx.LeftAlign.Add(((HexadecimalInt16)seq.Elements[2]).GetBytes);
        }

        private static void GetBytesh16Alt3(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                GetBytesh16Alt2((Alternative)alternative.Element, ctx);
                return;
            }

            var sequence = (Sequence)alternative.Element;
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 2; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var trailer = (HexadecimalInt16)sequence.Elements[1];
            ctx.LeftAlign.Add(trailer.GetBytes);
        }

        private static void GetBytesh16Alt4(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                GetBytesh16Alt3((Alternative)alternative.Element, ctx);
                return;
            }

            var sequence = (Sequence)alternative.Element;
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 3; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var trailer = (HexadecimalInt16)sequence.Elements[1];
            ctx.LeftAlign.Add(trailer.GetBytes);
        }

        private static void GetBytesh16Alt5(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                GetBytesh16Alt4((Alternative)alternative.Element, ctx);
                return;
            }

            var sequence = (Sequence)alternative.Element;
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 4; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var trailer = (HexadecimalInt16)sequence.Elements[1];
            ctx.LeftAlign.Add(trailer.GetBytes);
        }

        private static void GetBytesh16Alt6(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                GetBytesh16Alt5((Alternative)alternative.Element, ctx);
                return;
            }

            var sequence = (Sequence)alternative.Element;
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 5; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var trailer = (HexadecimalInt16)sequence.Elements[1];
            ctx.LeftAlign.Add(trailer.GetBytes);
        }

        private static void GetBytesh16Alt7(Alternative alternative, BytesFactoryContext ctx)
        {
            if (alternative.Ordinal == 2)
            {
                GetBytesh16Alt6((Alternative)alternative.Element, ctx);
                return;
            }

            var sequence = (Sequence)alternative.Element;
            var rep = (Repetition)sequence.Elements[0];
            for (var i = 0; i < 6; i++)
            {
                var seq1 = (Sequence)rep.Elements[i];
                var h16 = (HexadecimalInt16)seq1.Elements[0];
                ctx.LeftAlign.Add(h16.GetBytes);
            }

            var trailer = (HexadecimalInt16)sequence.Elements[1];
            ctx.LeftAlign.Add(trailer.GetBytes);
        }

        private static string ConvertToIPv6Address(byte[] bytes)
        {
            var segments = new int[8];
            for (var i = 0; i < bytes.Length; i += 2)
            {
                segments[i / 2] = bytes[i] << 8 | bytes[i + 1];
            }

            Subset emptySubset = null;
            var emptySubsets = new List<Subset>();
            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i] == ushort.MinValue)
                {
                    if (emptySubset == null)
                    {
                        emptySubset = new Subset { StartIndex = i };
                    }

                    emptySubset.Length += 1;
                }
                else
                {
                    if (emptySubset != null)
                    {
                        emptySubsets.Add(emptySubset);
                        emptySubset = null;
                    }
                }
            }

            if (emptySubset != null)
            {
                emptySubsets.Add(emptySubset);
            }

            if (emptySubsets.Count == 0)
            {
                return string.Join(":", segments.Select(FormatHex));
            }

            Subset collapse = emptySubsets[0];
            if (emptySubsets.Count != 1)
            {
                for (var i = 1; i < emptySubsets.Count; i++)
                {
                    var subset = emptySubsets[i];
                    if (subset.Length > collapse.Length)
                    {
                        collapse = subset;
                    }
                }
            }

            var buffer = new StringBuilder(capacity: 39);
            for (var i = 0; i < collapse.StartIndex; i++)
            {
                buffer.AppendFormat("{0:x}:", segments[i]);
            }

            if (collapse.StartIndex == 0)
            {
                buffer.Append(':');
            }

            var collapseEndIndex = collapse.StartIndex + collapse.Length;
            for (var i = collapseEndIndex; i < segments.Length; i++)
            {
                buffer.AppendFormat(":{0:x}", segments[i]);
            }

            if (collapseEndIndex == segments.Length)
            {
                buffer.Append(':');
            }

            return buffer.ToString();
        }

        private static string FormatHex(int value)
        {
            return string.Format("{0:x}", value);
        }

        private class Subset
        {
            public int StartIndex { get; set; }
            public int Length { get; set; }
        }

        private class BytesFactoryContext
        {
            private readonly IList<BytesFactory> leftAlign = new List<BytesFactory>();

            private readonly IList<BytesFactory> rightAlign = new List<BytesFactory>();

            public IList<BytesFactory> LeftAlign
            {
                get
                {
                    return this.leftAlign;
                }
            }

            public IList<BytesFactory> RightAlign
            {
                get
                {
                    return this.rightAlign;
                }
            }

            public byte[] GetResult()
            {
                var result = new byte[16];
                if (this.LeftAlign.Count != 0)
                {
                    var l = this.LeftAlign.SelectMany(factory => factory()).ToArray();
                    l.CopyTo(result, 0);
                }

                if (this.RightAlign.Count != 0)
                {
                    var r = this.RightAlign.SelectMany(factory => factory()).ToArray();
                    r.CopyTo(result, 16 - r.Length);
                }

                return result;
            }
        }
    }
}