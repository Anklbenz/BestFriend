using UnityEngine;

public class Project : MonoBehaviour {
    public APIOpenAI apiHandler;
    public ScenesLoader scenesLoader;
    public SessionCache sessionCache;
    
    public void Initialize(){
        Application.targetFrameRate = 80;
        scenesLoader.Initialize();
        apiHandler.Initialize();
        sessionCache = new SessionCache();
    }
}