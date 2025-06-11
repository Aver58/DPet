using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Scripts.Framework.UI {
    public static class ImageExtension {
        public static string SpriteAtlasDirectory = "Assets/ToBundle/Atlas";
        private static readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, SpriteAtlas> atlasMap = new Dictionary<string, SpriteAtlas>();

        public static void SetSprite(this Image image, string spriteName, bool setNativeSize = false) {
            if (image == null) {
                throw new NullReferenceException();
            }

            if (string.IsNullOrEmpty(spriteName)) {
                Debug.LogError($"[SetSprite] 传入的 spriteName 为空！");
                return;
            }

            var config = SpriteConfig.Get(spriteName);
            if (config == null) {
                return;
            }

            if (sprites.TryGetValue(spriteName, out var sprite)) {
                image.sprite = sprite;
            }

            var atlasName = GetSpriteName(config.atlasId);
            if (atlasMap.TryGetValue(atlasName, out var atlas) && atlas != null) {
                sprites[spriteName] = atlas.GetSprite(spriteName);
                image.sprite = sprites[spriteName];
            }

            var atlasPath = GetSpriteAtlasAssetPath(atlasName);
            var handle = LoadManager.LoadAssetAsync<SpriteAtlas>(atlasPath);
            handle.Completed += operation => {
                if (operation.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded) {
                    var loadedAtlas = operation.Result;
                    atlasMap[atlasName] = loadedAtlas;
                    sprites[spriteName] = loadedAtlas.GetSprite(spriteName);
                    image.sprite = sprites[spriteName];
                    if (setNativeSize) {
                        image.SetNativeSize();
                    }
                } else {
                    Debug.LogError($"Failed to load sprite atlas: {atlasPath}");
                }
            };
        }

        private static string GetSpriteName(int atlasId) {
            var atlasConfig = SpriteAtlasConfig.Get(atlasId.ToString());
            return atlasConfig?.name;
        }

        private static string GetSpriteAtlasAssetPath(string assetName) {
            return $"{SpriteAtlasDirectory}/{assetName}.spriteatlas";
        }
    }
}