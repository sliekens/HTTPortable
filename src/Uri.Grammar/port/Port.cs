namespace Uri.Grammar
{
    using System;

    using TextFx.ABNF;

    public class Port : Repetition
    {
        public Port(Repetition repetition)
            : base(repetition)
        {
        }

        public int ToInt()
        {
            return Convert.ToInt32(Text);
        }
    }
}