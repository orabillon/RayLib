using Raylib_cs;
using static Raylib_cs.Raylib;

namespace TemplateRaylib.ORN;

public class AssetsManager
{
    public static Font MainFont { get; private set; }

    public static void Load() {
        MainFont = LoadFont("PixelMaster");
    }
}