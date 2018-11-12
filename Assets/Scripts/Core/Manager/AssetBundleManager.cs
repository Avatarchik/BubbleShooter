//using UnityEngine;
//using LimboFramework.Loader;


//public class AssetBundleManager : MonoBehaviour
//{
//    private AssetBundleLoader _loader;

//    private void Start()
//    {
//        _loader = new AssetBundleLoader($"{Application.dataPath}/../AssetBundles/StandaloneWindows/test.assetbundle");

//        _loader.OnComplete += OnComplete;
//        _loader.OnProgress += pro => { Debug.Log(pro); };
//        _loader.Start();
//    }

//    private void Update()
//    {
//        _loader?.Update();
//    }

//    private void OnComplete(ILoader loader)
//    {
//        _loader = null;
//        AssetBundle bundle = loader.LoadedData as AssetBundle;
//        GameObject go = bundle.LoadAsset<GameObject>("Test");
//        //Instantiate(go);
//        Debug.Log("  sssssssss  ");
//    }

//    private async void Ins(GameObject go)
//    {
//        await Instantiate(go);
//    }
//}
