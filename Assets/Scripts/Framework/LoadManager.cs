using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework {
    public class LoadManager {
        public static AsyncOperationHandle<T> LoadAssetAsync<T>(string fullPath) {
            if (string.IsNullOrEmpty(fullPath)) {
                Debug.LogError("LoadAssetAsync: fullPath is null or empty.");
                return default;
            }

            fullPath = fullPath.Replace("\\", "/");
            fullPath = fullPath.Replace("\r", "");
            Debug.Log($"Loading asset: {fullPath}");
            var handle = Addressables.LoadAssetAsync<T>(fullPath);
            handle.Completed += operation => {
                if (operation.Status == AsyncOperationStatus.Succeeded) {
                    Debug.Log($"Successfully loaded asset: {fullPath}");
                } else {
                    Debug.LogError($"Failed to load asset: {fullPath}");
                }
            };

            return handle;
        }
    }
}