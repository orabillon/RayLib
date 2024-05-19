
using System.Diagnostics;
using Raylib_cs;

namespace TemplateRaylib.ORN;

/// <summary>
/// Classe pour la gestion des texture de raylib. Permmet de faciliter les test au chargement et
/// la liberation de la memoire 
/// </summary>
public class TextureRl
{
    public Texture2D Texture { get; private set; }
    private readonly string _name;

    public TextureRl(string pFileName)
    {
        _name = pFileName;
        Texture = Raylib.LoadTexture(pFileName);
        Debug.Assert(Texture.Width > 0, "Erreur de chargement de l'image " + pFileName);
    }

    public void Free()
    {
        Debug.WriteLine("Libere " + _name);
        Raylib.UnloadTexture(Texture);
    }
}