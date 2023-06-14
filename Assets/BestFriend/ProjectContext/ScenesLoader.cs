using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ScenesLoader {
    [SerializeField] private int loginSceneIndex;
    [SerializeField] private int userSceneIndex;
    [SerializeField] private int arWorkSceneIndex;
    public static ScenesLoader instance;

    public void Initialize() =>
        instance ??= this;

    public void GoToLoginScene() =>
        SceneManager.LoadScene(loginSceneIndex, LoadSceneMode.Single);

    public void GoToUserScene() =>
        SceneManager.LoadScene(userSceneIndex, LoadSceneMode.Single);

    public void GoToWorkArScene() =>
        SceneManager.LoadScene(arWorkSceneIndex, LoadSceneMode.Single);

    public void UnloadWorkArScene() =>
		    SceneManager.UnloadSceneAsync(arWorkSceneIndex);
}
