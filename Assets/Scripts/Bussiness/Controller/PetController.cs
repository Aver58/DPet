using System;
using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Bussiness.Controller {
    public class PetController : ControllerBase {
        public Action<int> OnInputCountChange;
        public Action<int> OnGoldCountChange;
        private int ClickGoldMin = 1;
        private int ClickGoldMax = 3;

        private int inputCount = 0;
        public int InputCount {
            get => inputCount;
            set {
                inputCount = value;
                OnInputCountChange?.Invoke(value);
                // CheckGetInputReward();
                AddGold();
            }
        }

        private int goldCount = 0;
        public int GoldCount {
            get => goldCount;
            set {
                goldCount = value;
                OnGoldCountChange?.Invoke(value);
            }
        }

        public PetController() {
            // InitInputRewardMap();
            // InitGiftCountMap();
        }

        // 金币获取
        private float randomEventProbability = 0.001f; // 0.1% 概率
        private void AddGold() {
            //随机事件奖励(0.1%概率)
            if (Random.Range(0f, 1f) < randomEventProbability) {
                int bonus = 100;
                // todo 动画表现
                AddCoin(bonus);
                Debug.Log("触发幸运金币，额外奖励：" + bonus);
            }

            // 阶段性奖励读取配置
            if (inputCount % 1000 == 0) {
                int stageBonus = Random.Range(100, 200);
                AddCoin(stageBonus);
                Debug.Log("达到输入里程碑，额外奖励：" + stageBonus);
            }

            var randomGold = UnityEngine.Random.Range(ClickGoldMin, ClickGoldMax + 1);
            GoldCount += randomGold;
            Debug.Log($"获得金币: {randomGold} 当前金币: {GoldCount}");
        }

        private void AddCoin(int count) {
            GoldCount += count;
        }

        #region Obsolete Gift

        private List<int> getRewardInputCountList = new List<int>(5);
        public Action<int> OnGiftCountChange;
        private Dictionary<int,int> giftCountMap = new Dictionary<int, int>(5){ };
        private int giftCount = 0;
        public int GiftCount {
            get => giftCount;
            private set {
                giftCount = value;
                OnGiftCountChange?.Invoke(value);
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

        private void InitGiftCountMap() {
            var keys = GiftTableConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                if (int.TryParse(key, out int giftId)) {
                    giftCountMap[giftId] = 0;
                } else {
                    Debug.LogError($"Invalid GiftType: {key}");
                }
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

        public enum GiftType {
            A = 1,
            B,
            C,
            D,
            E,
        }

        #endregion
    }
}