using System;
using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class PetController : ControllerBase {
        public Action<int> OnInputCountChange;
        public Action<int> OnGiftCountChange;
        private List<int> getRewardInputCountList = new List<int>(5);

        private int inputCount = 0;
        public int InputCount {
            get => inputCount;
            set {
                inputCount = value;
                OnInputCountChange?.Invoke(value);
                CheckGetInputReward();
            }
        }

        private int giftCount = 0;
        public int GiftCount {
            get => giftCount;
            private set {
                giftCount = value;
                OnGiftCountChange?.Invoke(value);
            }
        }

        private Dictionary<int,int> giftCountMap = new Dictionary<int, int>(5){ };

        public PetController() {
            InitInputRewardMap();
            InitGiftCountMap();
        }

        private void InitGiftCountMap() {
            var keys = GiftTableConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                if (int.TryParse(key, out int giftId)) {
                    // todo 获取缓存
                    giftCountMap[giftId] = 0;
                } else {
                    Debug.LogError($"Invalid GiftType: {key}");
                }
            }
        }

        private void InitInputRewardMap() {
            var keys = InputRewardConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                var count = int.Parse(key);
                getRewardInputCountList.Add(count);
            }
        }

        private void CheckGetInputReward() {
            for (int i = getRewardInputCountList.Count - 1; i >= 0; i--) {
                var getRewardInputCount = getRewardInputCountList[i];
                var isTriggered = InputCount % getRewardInputCount == 0;
                if (isTriggered) {
                    var config = InputRewardConfig.Get(getRewardInputCount.ToString());
                    if (config != null) {
                        var giftId = config.giftId;
                        GiftCount++;
                        giftCountMap[giftId]++;
                        break;
                    }

                    Debug.LogError($"InputRewardConfig not found for input count: {getRewardInputCount}");
                }
            }
        }

        public void GetAllReward() {
            foreach (var keyValuePair in giftCountMap) {
                var giftId = keyValuePair.Key;
                var count = keyValuePair.Value;
                for (int i = 0; i < count; i++) {
                    GetInputReward(giftId);
                }
            }

            // 清空奖励计数
            var keys = new List<int>(giftCountMap.Keys);
            foreach (var key in keys) {
                giftCountMap[key] = 0;
            }
        }
        
        private void GetInputReward(int giftId) {
            var config = GiftTableConfig.Get(giftId.ToString());
            if (config == null) {
                Debug.LogError($"GiftTableConfig not found for giftId: {giftId}");
                return;
            }

            var petPool = config.petPool;
            if (petPool.Length > 0) {
                var randomIndex = UnityEngine.Random.Range(0, petPool.Length);
                var petId = petPool[randomIndex];
                Debug.Log($"获得新宠物: petId : {petId} todo 获得宠物界面，或者商店界面，点击数可以购买新皮肤");
            } else {
                Debug.LogWarning($"No pets available in the pool for giftId: {giftId}");
            }
        }
    }

    public enum GiftType {
        A = 1,
        B,
        C,
        D,
        E,
    }
}