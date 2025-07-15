using System;
using System.Collections.Generic;

[System.Serializable]
public partial class PetMapConfig : BaseConfig {
    public int id;
    public string sprite1;
    public string sprite2;
    public string sprite3;
    public int maxLv;
    public int quality;
    public float clickAdd;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = int.Parse(values[i]);
                    break;
                case "sprite1":
                    sprite1 = values[i].Trim();
                    break;
                case "sprite2":
                    sprite2 = values[i].Trim();
                    break;
                case "sprite3":
                    sprite3 = values[i].Trim();
                    break;
                case "maxLv":
                    maxLv = int.Parse(values[i]);
                    break;
                case "Quality":
                    quality = int.Parse(values[i]);
                    break;
                case "clickAdd":
                    clickAdd = float.Parse(values[i]);
                    break;
            }
        }
    }

    private static Dictionary<string, PetMapConfig> cachedConfigs;
    public static PetMapConfig Get(int key) {
        return Get(key.ToString());
    }

    public static PetMapConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<PetMapConfig>("PetMap.csv");
        }

        PetMapConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("PetMap.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<PetMapConfig>("PetMap.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
