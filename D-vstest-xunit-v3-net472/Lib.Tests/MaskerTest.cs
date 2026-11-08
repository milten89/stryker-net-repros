using Lib;

public class MaskerTest
{
    [Fact] public void Mask_Long_ShowsLastFour() => Assert.Equal("*****6789", Masker.Mask("123456789"));
    [Fact] public void Mask_Short_HidesAll() => Assert.Equal("***", Masker.Mask("123"));
}
