using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnitySupport.XR
{
    public static class InstallationScript
    {
        private const string XRSettingsInstalledKey = "XRSETTINGS_INSTALLED";

        private static string XRSettingsInstalledPath
        {
            get
            {
                return Path.Combine("Library", XRSettingsInstalledKey);
            }
        }

        private static bool XRSettingsInstalled
        {
            get
            {
                return File.Exists(XRSettingsInstalledPath);
            }
        }

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            //if (XRSettingsInstalled)
            //{
            //    return;
            //}

            //CopySettingsToAssets();
        }

        [MenuItem("Unity Support/Reset XR Settings")]
        public static void ResetSettings()
        {
            // Todo: delete existing?

            CopySettingsToAssets();
        }

        public static void CopySettingsToAssets()
        {
            // Todo: do copy

            File.WriteAllText(XRSettingsInstalledPath, XRSettingsInstalledKey);
        }

        [MenuItem("Unity Support/Test")]
        public static void Test()
        {
            string absolutePath = Path.GetFullPath("Packages/com.unitysupport.xr");

            var exists = Directory.Exists(absolutePath);

            Debug.Log($"{exists}, {absolutePath}");

            absolutePath = Path.GetFullPath("Packages/com.unitysupport.xr/XR~");

            exists = Directory.Exists(absolutePath);

            Debug.Log($"{exists}, {absolutePath}");

            //AssetDatabase.Refresh();
        }
    }
}
