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
        private const string XRSettingsPackagePath = "Packages/com.unitysupport.xr/XR~";
        private const string XRSettingsAssetsPath = "Assets/XR";

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
            if (XRSettingsInstalled)
            {
                return;
            }

            CopySettingsToAssets();
        }

        [MenuItem("Unity Support/Reset XR Settings")]
        public static void ResetSettings()
        {
            CopySettingsToAssets();
        }

        public static void CopySettingsToAssets()
        {
            AssetDatabase.StartAssetEditing();

            AssetDatabase.DeleteAsset(XRSettingsAssetsPath);

            string sourcePath = Path.GetFullPath(XRSettingsPackagePath);
            CopyDirectory(sourcePath, XRSettingsAssetsPath, true);

            AssetDatabase.StopAssetEditing();

            AssetDatabase.Refresh();

            File.WriteAllText(XRSettingsInstalledPath, XRSettingsInstalledKey);
        }

        private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}
