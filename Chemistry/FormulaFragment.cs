namespace Chemistry;

public class FormulaFragment(string chemicalElement, int count)
{
    public string ChemicalElement => chemicalElement;

    public int Count => count;

    public override string ToString() => $"{ChemicalElement}:{Count}";
}