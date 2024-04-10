using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Chemistry;

public class FragmentParser 
{
private readonly string elementWithCount;
    public FragmentParser(string elementWithCount) 
    {
        this.elementWithCount = elementWithCount;
    }
    
    public string Parse() 
    {  
        return $"{ReadElement()}:{ReadCount()}";
    }

    private string ReadElement()
    {
        return elementWithCount.Substring(0, ElementLength());
    }

    private string ReadCount()
    {
        return ElementLength() < elementWithCount.Length ? elementWithCount.Substring(ElementLength()) : "1";
    }

    private int ElementLength()
    {
        int result = 0;
        while (result < elementWithCount.Length)
        {
            if (char.IsDigit(elementWithCount[result]))
            {
                break;
            }
            result++;
        }

        return result;
    }    
}
public class FormulaParser 
{
    public static string Parse(string formula)
    {
        return new FormulaParser(formula).Parse();
    }

    private readonly string formula;
    private FormulaParser(string formula) 
    {
        this.formula = formula;
    }
    
    private string Parse() 
    {  
        var fragments = GetFragments();

        return string.Join(',', fragments
        .Select(fragment => new FragmentParser(fragment).Parse())
        .Order());
    }

    private IEnumerable<string> GetFragments() 
    {
        return new Regex(@"([A-Z][a-z]*\d*)").Matches(formula)
                        .Cast<Match>()
                        .Select(match => match.Value)
                        .ToArray();
    }
}