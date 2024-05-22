using System.Numerics;
using Raylib_cs;

namespace MonSnake.ORN;

/// <summary>
/// Interface pour les éléments affichable  
/// </summary>
public interface IActor
{
    /// <summary>
    /// Position de l'élément à l'écran
    /// </summary>
    Vector2 Position { get; }
    
    /// <summary>
    /// Rectangle pour gérer la zone de collision de l'élément. 
    /// </summary>
    Rectangle BoundingBox { get; }

    /// <summary>
    /// Indique si l'élément peut être supprimer. 
    /// </summary>
    bool ToRemove { get; set; }
    
    /// <summary>
    /// Indique si une collision a été traitr 
    /// </summary>
    bool ToCollide { get; set; }

    /// <summary>
    /// Mise à jour de l'élément 
    /// </summary>
    /// <param name="dt"> Temps en seconde depuis le dernier update</param>
    void Update(float dt);

    /// <summary>
    /// Dessin de l'élément à l'écran
    /// </summary>
    void Draw();   

    /// <summary>
    /// Permet de définir un comportement de l'élément lors d'une collision
    /// </summary>
    /// <param name="pBy"> Élément avec lequel la collision a lieu</param>
    void TouchedBy(IActor pBy);
}