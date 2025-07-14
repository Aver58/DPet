using System.Collections.Generic;

public partial class PetMapConfig {
    public static List<int> GetPetPool(int quality) {
        List<int> petPool = new List<int>();
        var keys = GetKeys();
        for (int i = 0; i < keys.Count; i++) {
            var key = keys[i];
            var config = Get(key);
            if (config != null && config.quality == quality) {
                petPool.Add(config.id);
            }
        }
        return petPool;
    }
}