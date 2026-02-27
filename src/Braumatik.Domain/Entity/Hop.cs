using Braumatik.Domain.ValueObject;
using System.Timers;

namespace Braumatik.Domain.Entity;

public class Hop : Ingredient
{
    public Mass Weight { get; set; }
    public double AlphaAcids { get; set; }
    public HopUsage Usage { get; set; }
    public HopForm Form { get; set; }
    public Time Time { get; set; }

    private Hop(string name, Mass weight, double alphaAcids, HopUsage usage, HopForm form, Time time)
        : base(name)
    {
        Weight = weight;
        AlphaAcids = alphaAcids;
        Usage = usage;
        Form = form;
        Time = time;
    }

    public static Hop Create(string name, Mass weight, double alphaAcids, HopUsage usage, HopForm form, Time time)
        => new(name, weight, alphaAcids, usage, form, time);

    public double Bitterness(double earlyOg, Volume batchSize)
    {
        if (Usage == HopUsage.DryHop) return 0.0;

        // W = Weight of hops (oz), AA = Alpha Acid %, V = Volume (gal)
        // U = 1.65 × 0.000125^(SG-1) × (1 - e^(-0.04 × time)) / 4.15
        var batchSizeGallons = batchSize.ToGallons().Value;
        var weightInOunces = Weight.ConvertTo(MassUnit.Ounce).Value;
        var timeInMinutes = Time.ConvertTo(TimeUnit.Minute).Value;
        var u = 1.65 * Math.Pow(0.000125, earlyOg - 1) * (1 - Math.Pow(Math.E, -0.04 * timeInMinutes)) / 4.15;
        if (Usage != HopUsage.Boil)
        {
            // Adjust for hop stand and flameout
            u *= 0.5; // This is a simplification; actual utilization can vary
        }
        var ibu = (weightInOunces * AlphaAcids * u * 74.62) / batchSizeGallons;
        return Math.Round(ibu, 1);
    }
}
