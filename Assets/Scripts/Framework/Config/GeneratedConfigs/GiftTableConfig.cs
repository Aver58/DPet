using System;
using System.Collections.Generic;

[System.Serializable]
public class GiftTableConfig : BaseConfig {
    public int id;
    public int[] petPool;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = int.Parse(values[i]);
                    break;
                case "petPool":
                    petPool = Array.ConvertAll(values[i].Split(';'), int.Parse);
                    break;
            }
        }
    }

    private static Dictionary<string, GiftTableConfig> cachedConfigs;
    public static GiftTableConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<GiftTableConfig>("GiftTable.csv");
        }

        GiftTableConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("GiftTableConfig.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<GiftTableConfig>("GiftTable.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
