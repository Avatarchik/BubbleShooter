using System;
using System.IO;
using System.Text;
using LimboFramework.Utils;
using LitJson;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace LimboFramework.Editor
{
    public static class PublishHelper
    {
        private class PublishDescriptor
        {
            public string RemoteSettingUrl { get; set; }
            public string GameVersion { get; set; }
            public string ResVersion { get; set; }
            public string BundleIdentifer { get; set; }
            public int BuildVersionCode { get; set; }
        }

        [MenuItem("Tools/LoadConfig")]
        private static void Build()
        {
            string config = LoadConfig();
            PublishDescriptor publishDescriptor = JsonMapper.ToObject<PublishDescriptor>(config);
            SetIdentification(publishDescriptor);
        }

        private static string LoadConfig()
        {
            string configPath = $"{Application.dataPath}/../Config/{PlatformHelper.GetPlatformString()}BuildSettings.json";
            StringBuilder sBuilder = new StringBuilder();
            using (StreamReader reader = File.OpenText(configPath))
            {
                string line;
               
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    sBuilder.Append(line);
                }
            }

            return sBuilder.ToString();
        }

        private static void SetIdentification(PublishDescriptor publishDescriptor)
        {
            PlayerSettings.bundleVersion = publishDescriptor.GameVersion;
            PlayerSettings.applicationIdentifier = publishDescriptor.BundleIdentifer;
            PlayerSettings.iOS.buildNumber = publishDescriptor.BuildVersionCode.ToString();
            PlayerSettings.Android.bundleVersionCode = publishDescriptor.BuildVersionCode;
        }
    }
}