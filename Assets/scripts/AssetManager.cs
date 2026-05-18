using UnityEngine;
using System.Collections.Generic;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance { get; private set; }
    public List<GameObject> assets;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public GameObject GetAsset(string assetName)
    {
        foreach (GameObject asset in assets)
        {
            if (asset.name == assetName)
            {
                return asset;
            }
        }
        Debug.LogWarning($"Asset with name {assetName} not found.");
        return null;
    }
}
