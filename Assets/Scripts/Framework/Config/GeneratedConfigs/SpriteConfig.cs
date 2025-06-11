using System;
using System.Collections.Generic;

[System.Serializable]
public class SpriteConfig : BaseConfig {
    public string id;
    public int atlasId;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = values[i].Trim();
                    break;
                case "AtlasId":
                    atlasId = int.Parse(values[i]);
                    break;
            }
        }
    }

    private static Dictionary<string, SpriteConfig> cachedConfigs;
    public static SpriteConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<SpriteConfig>("Sprite.csv");
        }

        SpriteConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("SpriteConfig.csv not fount key" + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<SpriteConfig>("Sprite.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
