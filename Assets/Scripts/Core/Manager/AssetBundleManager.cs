using UnityEngine;
using LimboFramework.IO.Loader;


public class AssetBundleManager : MonoBehaviour
{
    private AssetBundleLoader _loader;

    private void Start()
    {
        _loader = new AssetBundleLoader($"{Application.dataPath}/../AssetBundles/StandaloneWindows/test.unity3d");

        _loader.OnComplete += OnComplete;
        _loader.Start();
    }

    private void Update()
    {
        _loader?.Update();
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
