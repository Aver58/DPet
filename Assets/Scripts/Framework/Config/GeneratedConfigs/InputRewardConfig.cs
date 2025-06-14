using System;
using System.Collections.Generic;

[System.Serializable]
public class InputRewardConfig : BaseConfig {
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

    private static Dictionary<string, InputRewardConfig> cachedConfigs;
    public static InputRewardConfig Get(string key) {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<InputRewardConfig>("InputReward.csv");
        }

        InputRewardConfig config = null;
        if (cachedConfigs != null) {
            cachedConfigs.TryGetValue(key, out config);
        }

        if (config == null) {
            UnityEngine.Debug.LogError("InputRewardConfig.csv not fount key : " + key);
            return null;
        }

        return config;
    }

    public static List<string> GetKeys() {
        if (cachedConfigs == null) {
            cachedConfigs = ConfigManager.Instance.LoadConfig<InputRewardConfig>("InputReward.csv");
        }

        return cachedConfigs != null ? new List<string>(cachedConfigs.Keys) : new List<string>();
    }
}
