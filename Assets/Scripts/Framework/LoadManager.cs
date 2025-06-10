using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Framework {
    public class LoadManager {
        public static AsyncOperationHandle LoadAssetAsync<T>(string fullPath) {
            return Addressables.LoadAssetAsync<T>(fullPath);
        }
    }
}