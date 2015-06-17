namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class HexadecimalInt16 : Repetition
    {
        public HexadecimalInt16(Repetition sequence)
            : base(sequence)
        {
        }
    }
}