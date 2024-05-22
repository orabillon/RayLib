using Raylib_cs;

namespace MonSnake.ORN;

/// <summary>
/// Définition de la structure d'une scène, d'un écran 
/// </summary>
public class Scene
{
    /// <summary>
    /// Liste des éléments affichable sur l'écran de la scène en cour
    /// </summary>
    protected List<IActor> listActors;

    protected Scene() {
        listActors = new List<IActor>();
    }

    /// <summary>
    /// Méthode pour le chargement des éléments de la scène
    /// </summary>
    public virtual void Load() { }
    
    /// <summary>
    /// Méthode pour décharger les éléments de la scène 
    /// </summary>
    public virtual void UnLoad() { }
    
    /// <summary>
    /// Méthode de mise à jour des éléments de la scène 
    /// </summary>
    /// <param name="dt"> Temps en seconde depuis le dernier update</param>
    public virtual void Update(float dt) 
    {
        foreach (var actor in listActors)
        {
            actor.Update(dt); 
        }
    }
    
    /// <summary>
    /// Méthode de dessin de l'écran
    /// </summary>
    public virtual void Draw() 
    {
        foreach (var actor in listActors)
        {
            actor.Draw();   
        }
    }

    /// <summary>
    /// Méthode de suppression des éléments affichable  
    /// </summary>
    public void Clean()
    {
        listActors.RemoveAll(item => item.ToRemove == true);
    }
    
}