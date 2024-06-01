
using System.Diagnostics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MonSnake.ORN;

/// <summary>
/// Classe pour la gestion des musique de raylib. Permmet de faciliter les test au chargement et
/// la liberation de la memoire 
/// </summary>
public class MusicRl
{
    public Music Music { get; private set; }
    private readonly string _name;

    public MusicRl(string pFileName)
    {
        _name = pFileName;
        Music = LoadMusicStream(pFileName);
        Debug.Assert(Music.FrameCount > 0, "Erreur de chargement de la musique " + pFileName);
    }

    public void Free()
    {
        Debug.WriteLine("Libere " + _name);
        UnloadMusicStream(Music);
    }
}