
using System.Diagnostics;
using Raylib_cs;

namespace MonSnake.ORN;

/// <summary>
/// Classe pour la gestion des Font de raylib. Permmet de faciliter les test au chargement et
/// la liberation de la memoire 
/// </summary>
public class FontRl
{
    public Font Font { get; private set; }
    private readonly string _name;

    public FontRl(string pFileName,int pSize)
    {
        _name = pFileName;
        Font = Raylib.LoadFontEx(pFileName,pSize,null,0);
        Debug.Assert(Font.Texture.Width > 0, "Erreur de chargement de la police " + pFileName);
    }

    public void Free()
    {
        Debug.WriteLine("Libere " + _name);
        Raylib.UnloadFont(Font);
    }
}