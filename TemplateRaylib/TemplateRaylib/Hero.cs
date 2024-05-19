using System.Numerics;
using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class Hero : Sprite
{
    public float Energie { get; set; }
    
    public Hero(string pFilePath, int pX = 0, int pY = 0) : base(pFilePath)
    {
        Energie = 100;
        Position = new Vector2(pX, pY);
    }
    
    public override void TouchedBy(IActor pBy)
    {
        if (pBy != null)
        {
            if(pBy is Meteor m){
                Energie -= 0.1f;
            }
        }
    }
}