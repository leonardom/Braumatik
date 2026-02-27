using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.ValueObject;

public class TimeTest
{
    [Fact]
    public void Static_constructors_set_value_and_unit()
    {
        var s = Time.Seconds(30);
        s.Value.ShouldBe(30);
        s.Unit.ShouldBe(TimeUnit.Second);

        var m = Time.Minutes(2);
        m.Value.ShouldBe(2);
        m.Unit.ShouldBe(TimeUnit.Minute);

        var h = Time.Hours(1.5);
        h.Value.ShouldBe(1.5);
        h.Unit.ShouldBe(TimeUnit.Hour);
    }

    [Theory]
    [InlineData(120, TimeUnit.Second, TimeUnit.Minute, 2)]
    [InlineData(3600, TimeUnit.Second, TimeUnit.Hour, 1)]
    [InlineData(2, TimeUnit.Minute, TimeUnit.Second, 120)]
    [InlineData(90, TimeUnit.Minute, TimeUnit.Hour, 1.5)]
    [InlineData(1.5, TimeUnit.Hour, TimeUnit.Minute, 90)]
    [InlineData(0.5, TimeUnit.Hour, TimeUnit.Second, 1800)]
    public void ConvertTo_PerformsExpectedConversions(double value, TimeUnit from, TimeUnit to, double expected)
    {
        var time = from switch
        {
            TimeUnit.Second => Time.Seconds(value),
            TimeUnit.Minute => Time.Minutes(value),
            TimeUnit.Hour => Time.Hours(value),
            _ => throw new ArgumentOutOfRangeException(nameof(from), from, $"Unknown TimeUnit: {from}")
        };

        var converted = time.ConvertTo(to);

        converted.Unit.ShouldBe(to);
        converted.Value.ShouldBe(expected, 1e-6);
    }

    [Fact]
    public void ConvertTo_SameUnit_ReturnsSameInstance()
    {
        var original = Time.Minutes(5);

        var converted = original.ConvertTo(TimeUnit.Minute);

        converted.ShouldBeSameAs(original);
    }
}
