using Raylib_cs;

namespace MonSnake.ORN;

public class AssetsManager
{
    public static Font MainFont { get; private set; }

    public static void Load() {
        MainFont = Raylib.LoadFont("ec-bricks.ttf");
    }

    public static void Unload()
    {
        Raylib.UnloadFont(MainFont);
    }
}