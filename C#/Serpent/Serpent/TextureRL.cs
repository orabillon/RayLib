using Raylib_cs;
using System.Diagnostics;

namespace Serpent
{
    public class TextureRL
    {
        public Texture2D texture { get; private set; }
        private string _name;

        public TextureRL(string pFileName)
        {
            _name = pFileName;
            texture = Raylib.LoadTexture(pFileName);
            Debug.Assert(texture.Width > 0, "Erreur de chargement de l'image " + pFileName);
        }

        public void Free()
        {
            Debug.WriteLine("Libere " + _name);
            Raylib.UnloadTexture(texture);
        }
    }
}
