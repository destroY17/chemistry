namespace Chemistry;

public class FormulaFragment(string chemicalElement, int count)
{
    public string ChemicalElement { get; } = chemicalElement;

    public int Count { get; } = count;

    public override string ToString() => $"{ChemicalElement}:{Count}";
}