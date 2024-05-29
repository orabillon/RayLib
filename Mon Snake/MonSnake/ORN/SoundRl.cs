
using System.Diagnostics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace MonSnake.ORN;

/// <summary>
/// Classe pour la gestion des son de raylib. Permmet de faciliter les test au chargement et
/// la liberation de la memoire 
/// </summary>
public class SoundRl
{
    public Sound Sound { get; private set; }
    private readonly string _name;

    public SoundRl(string pFileName)
    {
        _name = pFileName;
        Sound = LoadSound(pFileName);
        Debug.Assert(Sound.FrameCount > 0, "Erreur de chargement du son " + pFileName);
    }

    public void Free()
    {
        Debug.WriteLine("Libere " + _name);
        UnloadSound(Sound);
    }
}