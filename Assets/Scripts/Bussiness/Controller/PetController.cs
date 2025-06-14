using System;
using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class PetController : ControllerBase {
        public Action<int> OnInputCountChange;
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
        private Dictionary<GiftType,int> giftCountMap = new Dictionary<GiftType, int>(5){ };

        public PetController() {
            InitInputRewardMap();
            InitGiftCountMap();
        }

        private void InitGiftCountMap() {
            var keys = GiftTableConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                if (Enum.TryParse(key, out GiftType giftType)) {
                    // todo 获取缓存
                    giftCountMap[giftType] = 0;
                } else {
                    Debug.LogError($"Invalid GiftType: {key}");
                }
            }
        }

        private void InitInputRewardMap() {
            var keys = InputRewardConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                var inputCount = int.Parse(key);
                getRewardInputCountList.Add(inputCount);
            }
        }

        private void CheckGetInputReward() {
            for (int i = 0; i < getRewardInputCountList.Count; i++) {
                var getRewardInputCount = getRewardInputCountList[i];
                var isTriggered = InputCount % getRewardInputCount == 0;
                if (isTriggered) {
                    var config = InputRewardConfig.Get(getRewardInputCount.ToString());
                    if (config != null) {
                        var giftId = config.giftId;
                        GetInputReward(giftId);
                        giftCount++;
                    } else {
                        Debug.LogError($"InputRewardConfig not found for input count: {getRewardInputCount}");
                    }
                }
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
                // PlayerPrefs.SetInt(PlayerPrefsManager.MainPetId, petId);
                Debug.Log($"获得新宠物: {petId}");
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