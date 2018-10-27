using UnityEngine;
using LimboFramework.IO.Loader;


public class AssetBundleManager : MonoBehaviour
{
    private AssetBundleLoader _loader;

    private void Start()
    {
        _loader = new AssetBundleLoader($"{Application.dataPath}/../AssetBundles/StandaloneOSXUniversal/test.unity3d");

        _loader.OnComplete += OnComplete;
        _loader.Start();
    }

    private void Update()
    {
        if (null != _loader)
        {
            _loader.Update();
        }
    }

    private void OnComplete(ILoader loader)
    {
        _loader = null;
        AssetBundle bundle = loader.LoadedData as AssetBundle;
        GameObject go = bundle.LoadAsset<GameObject>("Test");
        Instantiate(go);
        Debug.Log("  sssssssss  ");
    }
}
