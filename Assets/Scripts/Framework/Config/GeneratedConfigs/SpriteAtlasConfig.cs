using System;
using System.Collections.Generic;

[System.Serializable]
public class SpriteAtlasConfig : BaseConfig {
    public int id;
    public string name;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = int.Parse(values[i]);
                    break;
                case "Name":
                    name = values[i].Trim();
                    break;
            }
        }
    }

    private static Dictionary<string, SpriteAtlasConfig> cachedConfigs;
    public static SpriteAtlasConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<SpriteAtlasConfig>("SpriteAtlas.csv");
        }

        SpriteAtlasConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("SpriteAtlasConfig.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<SpriteAtlasConfig>("SpriteAtlas.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
