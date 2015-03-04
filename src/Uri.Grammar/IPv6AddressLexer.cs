namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Int16Colon = Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>;
    using Colon = Text.Scanning.Element;
    using Colons = Text.Scanning.Sequence<Text.Scanning.Element, Text.Scanning.Element>;

    public class IPv6AddressLexer : Lexer<IPv6Address>
    {
        private readonly ILexer<HexadecimalInt16> hexadecimalInt16Lexer;

        private readonly ILexer<LeastSignificantInt32> leastSignificantInt32Lexer;

        public IPv6AddressLexer()
            : this(new HexadecimalInt16Lexer(), new LeastSignificantInt32Lexer())
        {
        }

        public IPv6AddressLexer(ILexer<HexadecimalInt16> hexadecimalInt16Lexer, ILexer<LeastSignificantInt32> leastSignificantInt32Lexer)
            : base("IPv6address")
        {
            Contract.Requires(hexadecimalInt16Lexer != null);
            Contract.Requires(leastSignificantInt32Lexer != null);
            this.hexadecimalInt16Lexer = hexadecimalInt16Lexer;
            this.leastSignificantInt32Lexer = leastSignificantInt32Lexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPv6Address element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPv6Address);
                return false;
            }

            if (this.TryReadFirstPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSecondPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadThirdPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadFourthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadFifthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSixthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSeventhPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadEighthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadNinthPass(scanner, out element))
            {
                return true;
            }

            element = default(IPv6Address);
            return false;
        }

        private IList<Element> ReadOptionalPieces(ITextScanner scanner, int count)
        {
            if (count == 0)
            {
                return new List<Element>(0);
            }

            var elements = new List<Element>((count * 2) - 1);
            for (int i = 0, n = count - 1; i < n; i++)
            {
                Int16Colon int16Colon;
                if (!this.TryReadInt16AndSeparator(scanner, out int16Colon))
                {
                    break;
                }

                elements.Add(int16Colon);
            }

            HexadecimalInt16 int16;
            if (this.hexadecimalInt16Lexer.TryRead(scanner, out int16))
            {
                elements.Add(int16);
                return elements;
            }

            if (elements.Count == 0)
            {
                return new List<Element>(0);
            }

            for (int i = elements.Count - 1; i >= 0; i--)
            {
                scanner.PutBack(elements[i].Data);
            }

            return this.ReadOptionalPieces(scanner, count - 1);
        }

        private IList<Int16Colon> ReadRequiredPieces(ITextScanner scanner, int count)
        {
            var elements = new List<Int16Colon>(count);
            for (int i = 0; i < count; i++)
            {
                Int16Colon int16Colon;
                if (this.TryReadInt16AndSeparator(scanner, out int16Colon))
                {
                    elements.Add(int16Colon);
                }
                else
                {
                    if (elements.Count != 0)
                    {
                        for (int j = elements.Count - 1; j >= 0; j--)
                        {
                            scanner.PutBack(elements[j].Data);
                        }
                    }

                    return null;
                }
            }

            return elements;
        }

        private bool TryReadFirstPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var bits = this.ReadRequiredPieces(scanner, 6);
            if (bits == null)
            {
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(bits[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(bits), lsbits), context);
            return true;
        }

        private bool TryReadSecondPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                element = default(IPv6Address);
                return false;
            }

            var bits = this.ReadRequiredPieces(scanner, 5);
            if (bits == null)
            {
                scanner.PutBack(colons.Data);
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(bits[i].Data);
                }

                scanner.PutBack(colons.Data);
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(colons, string.Concat(bits), lsbits), context);
            return true;
        }

        private bool TryReadThirdPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 1);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            var bits = this.ReadRequiredPieces(scanner, 4);
            if (bits == null)
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(bits[i].Data);
                }

                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i1 = optionalPieces.Count - 1; i1 >= 0; i1--)
                    {
                        scanner.PutBack(optionalPieces[i1].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, string.Concat(bits), lsbits), context);
            return true;
        }

        private bool TryReadFourthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 2);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            var bits = this.ReadRequiredPieces(scanner, 3);
            if (bits == null)
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(bits[i].Data);
                }

                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i1 = optionalPieces.Count - 1; i1 >= 0; i1--)
                    {
                        scanner.PutBack(optionalPieces[i1].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, string.Concat(bits), lsbits), context);
            return true;
        }

        private bool TryReadFifthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 3);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            var bits = this.ReadRequiredPieces(scanner, 2);
            if (bits == null)
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                for (int i = bits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(bits[i].Data);
                }

                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i1 = optionalPieces.Count - 1; i1 >= 0; i1--)
                    {
                        scanner.PutBack(optionalPieces[i1].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, string.Concat(bits), lsbits), context);
            return true;
        }

        private bool TryReadSixthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 4);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            Int16Colon bits;
            if (!this.TryReadInt16AndSeparator(scanner, out bits))
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                scanner.PutBack(bits.Data);
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, bits, lsbits), context);
            return true;
        }

        private bool TryReadSeventhPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 5);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 lsbits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out lsbits))
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, lsbits), context);
            return true;
        }

        private bool TryReadEighthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 6);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            HexadecimalInt16 lsbits;
            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out lsbits))
            {
                scanner.PutBack(colons.Data);
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons, lsbits), context);
            return true;
        }

        private bool TryReadNinthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            var optionalPieces = this.ReadOptionalPieces(scanner, 7);
            Colons colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (optionalPieces.Count != 0)
                {
                    for (int i = optionalPieces.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(optionalPieces[i].Data);
                    }
                }
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionalPieces), colons), context);
            return true;
        }

        private bool TryReadInt16AndSeparator(ITextScanner scanner, out Int16Colon element)
        {
            var context = scanner.GetContext();
            HexadecimalInt16 int16;
            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out int16))
            {
                element = default(Int16Colon);
                return false;
            }

            Colon colon;
            if (!this.TryReadColon(scanner, out colon))
            {
                scanner.PutBack(int16.Data);
                element = default(Int16Colon);
                return false;
            }

            element = new Int16Colon(int16, colon, context);
            return true;
        }

        private bool TryReadColons(ITextScanner scanner, out Colons element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Colons);
                return false;
            }

            var context = scanner.GetContext();
            Colon colon1, colon2;
            if (!this.TryReadColon(scanner, out colon1))
            {
                element = default(Colons);
                return false;
            }

            if (!this.TryReadColon(scanner, out colon2))
            {
                scanner.PutBack(colon1.Data);
                element = default(Colons);
                return false;
            }

            element = new Colons(colon1, colon2, context);
            return true;
        }

        private bool TryReadColon(ITextScanner scanner, out Colon element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Colon);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch(':'))
            {
                element = default(Colon);
                return false;
            }

            element = new Colon(":", context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexadecimalInt16Lexer != null);
            Contract.Invariant(this.leastSignificantInt32Lexer != null);
        }
    }
}