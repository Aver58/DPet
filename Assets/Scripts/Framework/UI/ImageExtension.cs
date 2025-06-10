using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Scripts.Framework.UI {
    public static class ImageExtension {
        private static readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, SpriteAtlas> atlasMap = new Dictionary<string, SpriteAtlas>();

        public static void SetSprite(this Image image, string assetName, bool setNativeSize = false) {
            if (image == null) {
                throw new NullReferenceException();
            }

            if (string.IsNullOrEmpty(assetName)) {
                Debug.LogError($"[SetSprite] 传入的 assetName 为空！");
                return;
            }

            // var config = UISpriteConfig.Get(assetName);
            // if (config == null) {
            //     Debug.LogErrorFormat("获取图片配置 iconKey:{0} 失败", assetName);
            //     return;
            // }

            // var spriteName = config.SpriteName;
            // if (string.IsNullOrEmpty(spriteName)) {
            //     return;
            // }

            // LoadSpriteAsync(image, spriteName);
        }

        private static void LoadSpriteAsync(Image image, string spriteName) {
            // if (!UISpriteConfig.Has(spriteName)) {
            //     return;
            // }
            //
            // var config = UISpriteConfig.Get(spriteName);
            // if (config == null) {
            //     return;
            // }
            //
            // if (sprites.ContainsKey(spriteName)) {
            //     image.sprite = sprites[spriteName];
            // }
            //
            // var atlasName = GetSpriteName(config.AtlasId);
            // if (atlasMap.TryGetValue(atlasName, out var atlas) && atlas != null) {
            //     sprites[spriteName] = atlas.GetSprite(spriteName);
            //     image.sprite = sprites[spriteName];
            // }
            //
            // var atlasPath = AssetUtility.GetUIAtlasAsset(atlasName);
            // GameEntry.Resource.LoadAsset(atlasPath, new LoadAssetCallbacks((assetName, asset, duration, userData) => {
            //     if (asset != null && asset is SpriteAtlas spriteAtlas) {
            //         atlasMap[atlasName] = spriteAtlas;
            //         sprites[spriteName] = spriteAtlas.GetSprite(spriteName);
            //         image.sprite = sprites[spriteName];
            //     }
            // }));
        }
    }
}