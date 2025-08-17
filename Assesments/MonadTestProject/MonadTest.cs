using System;

namespace Monad;

public class OptionTests
{
    private Option<SomeClass> defaultOption;

    [Fact]
    public void Empty_Option_Has_No_Value()
    {
        Assert.False(Option.Empty<SomeClass>().HasValue);
    }

    [Fact]
    public void Default_Option_IsEmpty()
    {
        Assert.Equal(Option.Empty<SomeClass>(), defaultOption);
    }

    [Fact]
    public void Non_Empty_Option_Has_Value()
    {
        Assert.True(Option.FromValue(new SomeClass()).HasValue);
    }

    [Fact]
    public void Empty_Option_Value_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => Option.Empty<SomeClass>().Value);
    }

    [Fact]
    public void Non_Empty_Option_Returns_Its_Value()
    {
        var value = new SomeClass();
        Assert.Same(value, Option.FromValue(value).Value);
    }

    [Fact]
    public void Comparison_Is_Correct_If_Not_Empty()
    {
        var someClass = new SomeClass();
        Assert.True(Option.FromValue(someClass) == Option.FromValue(someClass));
        Assert.False(Option.FromValue(someClass) == Option.Empty<SomeClass>());
    }

    [Fact]
    public void Empty_Option_ValueOr_Evaluates_Else_Branch()
    {
        var expected = new SomeClass();
        Assert.Same(expected, Option.Empty<SomeClass>().ValueOr(() => expected));
    }

    [Fact]
    public void Empty_Option_Select_Returns_Empty()
    {
        var result = Option.Empty<SomeClass>().Select(v => v.GetType());
        Assert.True(result == Option.Empty<Type>());
    }

    [Fact]
    public void Non_Empty_Option_Select_Returns_ExpectedType()
    {
        var result = Option.FromValue(new SomeClass()).Select(v => v.GetType());
        Assert.True(result == Option.FromValue(typeof(SomeClass)));
    }

    [Fact]
    public void Non_Empty_Option_ValueOr_Does_Not_Evaluate_Else_Branch()
    {
        var value = new object();
        var option = Option.FromValue(value);
        Func<SomeClass> fct = () =>
        {
            // we will break on purpose here.
            Assert.True(false);
            return null;
        };
        Assert.Same(value, option.ValueOr(fct));
    }

    private class SomeClass
    {
    }
}
