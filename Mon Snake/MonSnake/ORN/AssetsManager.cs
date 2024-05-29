using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MonSnake.ORN;

public class AssetsManager
{
    public static Font MainFont { get; private set; }

    public static void Load() {
        MainFont = LoadFont("ec-bricks.ttf");
    }

    public static void Unload()
    {
        UnloadFont(MainFont);
    }
}