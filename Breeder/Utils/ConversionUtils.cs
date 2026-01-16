namespace Breeder;

public class ConversionUtils
{
    public static decimal GrammesToKilogrammes(decimal grammes)
    {
        return grammes / 1000m;
    }

    public static decimal KilogrammesToGrammes(decimal kg)
    {
        return kg * 1000m;
    }
}