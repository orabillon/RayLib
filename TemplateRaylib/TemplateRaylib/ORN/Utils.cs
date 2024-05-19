using Raylib_cs;

namespace TemplateRaylib.ORN;

public static class Utils
{
    #region Générateur aleatoire

        static Random _randomGen = new Random();

        public static void SetRandomSeed(int pSeed)
        {
            _randomGen = new Random(pSeed);
        }

        public static int GetInt(int pMin, int pMax)
        {
            return _randomGen.Next(pMin, pMax + 1);
        }

    #endregion
    
    public static bool CollideByBox(IActor pActor1, IActor pActor2)
    {
        return Raylib.CheckCollisionRecs(pActor1.BoundingBox, pActor2.BoundingBox);
    }
}