using System;
using System.Collections.Generic;

[System.Serializable]
public class InputAwardConfig : BaseConfig {
    public int id;
    public int giftId;

    public override void Parse(string[] values, string[] headers) {
        for (int i = 0; i < headers.Length; i++) {
            var header = headers[i].Replace("\r", "");
            switch (header) {
                case "id":
                    id = int.Parse(values[i]);
                    break;
                case "giftId":
                    giftId = int.Parse(values[i]);
                    break;
            }
        }
    }

    private static Dictionary<string, InputAwardConfig> cachedConfigs;
    public static InputAwardConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<InputAwardConfig>("InputAward.csv");
        }

        InputAwardConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("InputAwardConfig.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<InputAwardConfig>("InputAward.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
