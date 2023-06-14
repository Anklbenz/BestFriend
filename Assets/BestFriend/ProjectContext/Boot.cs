using UnityEngine;

public class Boot : MonoBehaviour {
    [SerializeField] private Project project;
    
    private void Awake(){
        project.Initialize();
        ScenesLoader.instance.GoToLoginScene();
        DontDestroyOnLoad(project);
    }
}