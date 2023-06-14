using JsonDeserialize;
using UnityEngine;

public class SessionCache {
    public Asset prefab{ get; set; }
    public Texture2D qrcodeTexture{ get; set; }
    public long selectedAssetId{ get; set; } = -1;
    public AssetInfo assetInfo{ get; set; }
    public WorkLogFullInfo workLogFullInfo{ get; set; }
    public AnimationRequest animationData { get; set; }
    public string fbxFileName { get; set; }
    public bool hasAnimation { get; set; }

    public static SessionCache instance;

    public SessionCache(){
        if (instance is not null) return;
        instance = this;
    }

    public void Clear(){
        prefab = null;
        qrcodeTexture = null;
        selectedAssetId = -1;
    }
}