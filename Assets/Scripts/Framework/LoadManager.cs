using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework {
    public class LoadManager {
        // 缓存一下已加载对象
        private static Dictionary<string, object> loadedAssets = new Dictionary<string, object>();

        public static AsyncOperationHandle<T> LoadAssetAsync<T>(string fullPath) {
            if (string.IsNullOrEmpty(fullPath)) {
                Debug.LogError("LoadAssetAsync: fullPath is null or empty.");
                return default;
            }

            fullPath = fullPath.Replace("\\", "/");
            fullPath = fullPath.Replace("\r", "");
            Debug.Log($"[LoadManager]Start Load Asset Async: {fullPath}");
            var handle = Addressables.LoadAssetAsync<T>(fullPath);
            handle.Completed += operation => {
                if (operation.Status == AsyncOperationStatus.Succeeded) {
                    Debug.Log($"[LoadManager]Load Asset: {fullPath} Successfully!");
                } else {
                    Debug.LogError($"Failed to load asset: {fullPath}");
                }
            };

            return handle;
        }
    }
}