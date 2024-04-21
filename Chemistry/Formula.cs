namespace Chemistry;

public class Formula(IEnumerable<FormulaFragment> formulaFragments)
{
    public override string ToString()
    {
        var fragmentsAsString = formulaFragments.Select(fragment => fragment.ToString());
        return string.Join(',', fragmentsAsString.Order());
    }
}