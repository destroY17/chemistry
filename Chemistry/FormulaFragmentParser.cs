using System.Text.RegularExpressions;

namespace Chemistry;

public partial class FormulaFragmentParser
{
    [GeneratedRegex("[A-Z][a-z]?")]
    private static partial Regex ChemicalElementRegex();

    private readonly string _formulaFragment;

    private FormulaFragmentParser(string formulaFragment) => _formulaFragment = formulaFragment;
    
    public static IEnumerable<FormulaFragment> GetFragmentsFromString(string formulaFragment) => 
        new FormulaFragmentParser(formulaFragment).GetFragmentsFromString();

    private IEnumerable<FormulaFragment> GetFragmentsFromString()
    {
        return GetChemicalElements()
            .Select(chemicalElement => new FormulaFragment(chemicalElement, GetChemicalElementsCount()));
    }

    private IEnumerable<string> GetChemicalElements()
    {
        return ChemicalElementRegex().Matches(_formulaFragment[..GetChemicalElementLength()])
            .Select(match => match.Value);
    }

    private int GetChemicalElementsCount()
    {
        return GetChemicalElementLength() < _formulaFragment.Length
            ? int.Parse(_formulaFragment[GetChemicalElementLength()..])
            : 1;
    }
    
    private int GetChemicalElementLength() => _formulaFragment.TakeWhile(symbol => !char.IsDigit(symbol)).Count();
}