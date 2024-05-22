using Raylib_cs;

namespace MonSnake.ORN;

public class AssetsManager
{
    public static Font MainFont { get; private set; }

    public static void Load() {
        MainFont = Raylib.LoadFont("PixelMaster");
    }
}