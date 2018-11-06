using UnityEngine;

namespace LimboFramework.Version
{
    public class GameConfig : ScriptableObject
    {
        private static readonly string ConfigPath = "Assets/ThirdPart/LimboFramework/Resources/GameConfig.asset";
        private static GameConfig _instance;

        public string RemoteSettingUrl;

        public static GameConfig Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = Resources.Load<GameConfig>("GameConfig");
#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        _instance = CreateInstance<GameConfig>();
                        UnityEditor.AssetDatabase.CreateAsset(_instance, ConfigPath);
                        UnityEditor.AssetDatabase.SaveAssets();
                    }
#endif
                }

                return _instance;
            }
        }
    }
}