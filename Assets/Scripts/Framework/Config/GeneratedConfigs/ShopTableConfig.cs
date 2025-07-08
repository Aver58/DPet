using System;
using System.Collections.Generic;

[System.Serializable]
public class ShopTableConfig : BaseConfig {
    public int shopId;
    public string shopItemName;
    public string shopIcon;
    public float nProbability;
    public float rProbability;
    public float sRProbability;
    public float sSRProbability;
    public int price;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "shopId":
                    shopId = int.Parse(values[i]);
                    break;
                case "shopItemName":
                    shopItemName = values[i].Trim();
                    break;
                case "shopIcon":
                    shopIcon = values[i].Trim();
                    break;
                case "NProbability":
                    nProbability = float.Parse(values[i]);
                    break;
                case "RProbability":
                    rProbability = float.Parse(values[i]);
                    break;
                case "SRProbability":
                    sRProbability = float.Parse(values[i]);
                    break;
                case "SSRProbability":
                    sSRProbability = float.Parse(values[i]);
                    break;
                case "Price":
                    price = int.Parse(values[i]);
                    break;
            }
        }
    }

    private static Dictionary<string, ShopTableConfig> cachedConfigs;
    public static ShopTableConfig Get(int key) {
        return Get(key.ToString());
    }

    public static ShopTableConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<ShopTableConfig>("ShopTable.csv");
        }

        ShopTableConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("ShopTable.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<ShopTableConfig>("ShopTable.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
