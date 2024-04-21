using System.Text.RegularExpressions;

namespace Chemistry;

public partial class FormulaParser
{
    [GeneratedRegex(@"([A-Z][a-z]*)(\d*)|\(([^()]*)\)(\d*)")]
    private static partial Regex FormulaFragmentRegex();

    private readonly string _chemistryFormula;

    public static string Parse(string formula) => new FormulaParser(formula).Parse();

    private FormulaParser(string chemistryFormula) => _chemistryFormula = chemistryFormula;

    private string Parse() => new Formula(GetChemicalFragments(_chemistryFormula)).ToString();

    private IEnumerable<FormulaFragment> GetChemicalFragments(string chemistryFormula)
    {
        var chemicalFragments = FormulaFragmentRegex().Matches(chemistryFormula)
            .Select(match => match.Value)
            .SelectMany(FormulaFragmentParser.GetFragmentsFromString);
        
        return MergeFragmentsWithSameElements(chemicalFragments);
    }

    private IEnumerable<FormulaFragment> MergeFragmentsWithSameElements(IEnumerable<FormulaFragment> chemicalFragments)
    {
        return chemicalFragments
            .GroupBy(fragment => fragment.ChemicalElement)
            .Select(group => new FormulaFragment(group.Key, group.Sum(fragment => fragment.Count)));
    }
}