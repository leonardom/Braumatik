namespace Braumatik.Domain.Entity;

public class Recipe
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double BoilSize { get; set; }
    public double BatchSize { get; set; }
    public string? Style { get; set; }
    public IEnumerable<Fermentable> Fermentables { get; set; } = [];
}
