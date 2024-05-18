namespace TemplateRaylib.ORN;

public static class Utils
{
    static Random _randomGen = new Random();

    public static void SetRandomSeed(int pSeed)
    {
        _randomGen = new Random(pSeed);
    }

    public static int GetInt(int pMin, int pMax)
    {
        return _randomGen.Next(pMin, pMax + 1);
    }
}