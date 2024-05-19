namespace TemplateRaylib.ORN;

public class GameState
{
    /// <summary>
    ///Énumération des types de scène du jeu
    /// </summary>
    public enum SceneType
    {
        Menu,
        Gameplay,
        Gameover
    }
    
    /// <summary>
    /// Scène en cours d'exécution 
    /// </summary>
    public Scene CurrentScene { get; set; }
    
    /// <summary>
    /// Permet de changer la scène en cours dans le jeu
    /// </summary>
    /// <param name="pSceneType">Type de la scène a charger</param>
    public void ChangeScene(SceneType pSceneType)
    {
        if (CurrentScene != null)
        {
            CurrentScene.UnLoad();
            CurrentScene = null;
        }

        switch (pSceneType)
        {
            case SceneType.Menu:
                CurrentScene = new SceneMenu(this);
                break;
            case SceneType.Gameplay:
                CurrentScene = new SceneGamePlay(this);
                break;
            case SceneType.Gameover:
                CurrentScene = new SceneGameOver();
                break;
        }

        CurrentScene.Load();
    }
}