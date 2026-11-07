using Lib;

public class HandlerTest
{
    [Fact] public void Describe_One_NamesFirst() => Assert.Equal("First is 7", Handler.Describe([7]));
    [Fact] public void DescribeControl_One_NamesFirst() => Assert.Equal("First is 7", Handler.DescribeControl([7]));
}
