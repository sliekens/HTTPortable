namespace Uri.Grammar.gen_delims
{
    using Xunit;

    public class GenericDelimiterLexerTests
    {
        [Theory]
        [InlineData(@":")]
        [InlineData(@"/")]
        [InlineData(@"?")]
        [InlineData(@"#")]
        [InlineData(@"[")]
        [InlineData(@"]")]
        [InlineData(@"@")]
        public void Read_ShouldSucceed(string input)
        {
            
        }
    }
}
