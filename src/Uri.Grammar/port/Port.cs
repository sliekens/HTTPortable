namespace Uri.Grammar
{
    using System;

    using TextFx.ABNF;

    public class Port : Repetition
    {
        public Port(Repetition sequence)
            : base(sequence)
        {
        }

        public int ToInt()
        {
            return Convert.ToInt32(this.Value);
        }
    }
}