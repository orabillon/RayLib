using TemplateRaylib.ORN;

namespace TemplateRaylib;

public class Meteor : Sprite
{
    public Meteor(string pFilePath, int pX, int pY) : base(pFilePath)
    {
        do { VelociteX = Utils.GetInt(-3, 3) / 5.0f;} while(VelociteX==0);

        do { VelociteY = Utils.GetInt(-3, 3) / 5.0f; } while(VelociteY==0);
    }
}