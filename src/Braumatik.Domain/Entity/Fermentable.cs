using Braumatik.Domain.Entity;
using Braumatik.Domain.ValueObject;
using System.Text.RegularExpressions;

namespace Braumatik.Domain;

public partial class Fermentable : Ingredient
{

    [GeneratedRegex(@"candi|candy|dme|dry|extract|honey|lme|liquid|sugar|syrup|turbinado", RegexOptions.IgnoreCase)]
    private static partial Regex BoilRegex();

    [GeneratedRegex(@"biscuit|black|cara|chocolate|crystal|munich|roast|special ?b|toast|victory|vienna", RegexOptions.IgnoreCase)]
    private static partial Regex SteepRegex();

    public Mass Weight { get; private set; }
    public Yield Yield { get; private set; }
    public double Color { get; private set; } = .0;

    private Fermentable(string name, Mass weight, Yield? yield)
        : base(name)
    {
        Weight = weight;
        Yield = Yield.ValueOrDefault(yield);
    }

    public static Fermentable Create(string name, Mass weight, Yield? yield = null) => new(name, weight, yield);

    public string Type() => BoilRegex().IsMatch(Name) ? "Extract" : "Grain";

    public string Addition() => Name switch
    {
        _ when Regex.IsMatch(Name, @"mash", RegexOptions.IgnoreCase) => "Mash",
        _ when Regex.IsMatch(Name, @"steep", RegexOptions.IgnoreCase) => "Steep",
        _ when Regex.IsMatch(Name, @"boil", RegexOptions.IgnoreCase) => "Boil",
        _ when BoilRegex().IsMatch(Name) => "Boil",
        _ when SteepRegex().IsMatch(Name) => "Steep",
        _ => "Mash"
    };

    public Ppg YieldToPpg() => Ppg.FromYield(Yield);

    public GravityUnit GravityUnits(Volume volume) 
        => new(YieldToPpg().Value * Weight.ConvertTo(MassUnit.Pound).Value / volume.ToGallons().Value);
}
