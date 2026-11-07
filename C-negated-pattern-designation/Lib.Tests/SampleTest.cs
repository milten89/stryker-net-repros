using Lib;

public class SampleTest
{
    [Fact] public void Next_Value_Increments() => Assert.Equal(4, Sample.Next(3, 10));
    [Fact] public void Next_Null_DoublesFallback() => Assert.Equal(20, Sample.Next(null, 10));
    [Fact] public void NextControl_Value_Increments() => Assert.Equal(4, Sample.NextControl(3, 10));
    [Fact] public void NextControl_Null_DoublesFallback() => Assert.Equal(20, Sample.NextControl(null, 10));
}
