using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace TemplateRaylib.ORN;

public class Sprite : IActor
{
    // Iactor
    public Vector2 Position { get; set; }
    public Rectangle BoundingBox { get; private set; }
    public bool ToRemove { get; set; } = false;
    public bool ToCollide { get; set; } = false;

    // Sprite 
    public float VelociteX { get; set; }
    public float VelociteY { get; set; }
    public TextureRl Texture { get; }
    
    public Sprite(string pFilePath)
    {
        Texture = new TextureRl(pFilePath);
        VelociteX = 0;
        VelociteY = 0;
    }
    
    public virtual void Draw()
    {
        DrawTexture(Texture.Texture,(int)Position.X, (int)Position.Y, Color.White);
    }

    public virtual void Update(float dt)
    {
        Move(VelociteX, VelociteY);
        BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Texture.Width, Texture.Texture.Height);
    }

    public void Move(float pX, float pY)
    {
        Position = new Vector2(Position.X + pX, Position.Y + pY);
    }

    public virtual void TouchedBy(IActor pBy)
    {
            
    }

    public virtual void Unload()
    {
        Texture.Free();
    }
}