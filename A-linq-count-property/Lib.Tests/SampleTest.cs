using Lib;

public class SampleTest
{
    [Fact] public void IsSingle_One_True() => Assert.True(Sample.IsSingle([1]));
    [Fact] public void IsSingle_Two_False() => Assert.False(Sample.IsSingle([1, 2]));
    [Fact] public void IsSingleControl_One_True() => Assert.True(Sample.IsSingleControl([1]));
    [Fact] public void IsSingleControl_Two_False() => Assert.False(Sample.IsSingleControl([1, 2]));
}
