using System;
using System.Collections.Generic;

[System.Serializable]
public partial class PetMapConfig : BaseConfig {
    public int id;
    public string name;
    public int quality;
    public int maxLv;
    public float price;
    public string sprite1;
    public string sprite2;
    public string sprite3;
    public float lv1Add;
    public float lv2Add;
    public float lv3Add;
    public int lv1Times;
    public int lv2Times;

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
                case "Quality":
                    quality = int.Parse(values[i]);
                    break;
                case "maxLv":
                    maxLv = int.Parse(values[i]);
                    break;
                case "price":
                    price = float.Parse(values[i]);
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
                case "lv1Add":
                    lv1Add = float.Parse(values[i]);
                    break;
                case "lv2Add":
                    lv2Add = float.Parse(values[i]);
                    break;
                case "lv3Add":
                    lv3Add = float.Parse(values[i]);
                    break;
                case "lv1Times":
                    lv1Times = int.Parse(values[i]);
                    break;
                case "lv2Times":
                    lv2Times = int.Parse(values[i]);
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
