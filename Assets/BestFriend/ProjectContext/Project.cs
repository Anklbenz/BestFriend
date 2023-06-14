using UnityEngine;

public class Project : MonoBehaviour {
    public APIOpenAI openApi;
    public ScenesLoader scenesLoader;
    public SessionCache sessionCache;
    
    public void Initialize(){
        Application.targetFrameRate = 80;
        openApi = new APIOpenAI();
        scenesLoader.Initialize();
        openApi.Initialize();
        sessionCache = new SessionCache();
    }
}