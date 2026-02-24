namespace Braumatik.Domain.Entity;

public class Ingredient
{
    public string Name { get; protected set; }

    protected Ingredient(string Name) => this.Name = Name;
}
