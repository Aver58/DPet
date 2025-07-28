using System;
using System.Collections.Generic;

[System.Serializable]
public partial class PetMapConfig : BaseConfig {
    public int id;
    public string name;
    public int quality;
    public int stage;
    public int price;
    public string sprite;
    public float add;
    public int exp;
    public int levelUpId;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = int.Parse(values[i]);
                    break;
                case "name":
                    name = values[i].Trim();
                    break;
                case "quality":
                    quality = int.Parse(values[i]);
                    break;
                case "stage":
                    stage = int.Parse(values[i]);
                    break;
                case "price":
                    price = int.Parse(values[i]);
                    break;
                case "sprite":
                    sprite = values[i].Trim();
                    break;
                case "add":
                    add = float.Parse(values[i]);
                    break;
                case "exp":
                    exp = int.Parse(values[i]);
                    break;
                case "levelUpId":
                    levelUpId = int.Parse(values[i]);
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
