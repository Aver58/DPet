using System;
using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Bussiness.Controller {
    public class PetController : ControllerBase {
        private List<int> inputTimesList = new List<int>(6);
        private int currentRewardIndex = -1;

        public int CurrentRewardInputTimes = 0;
        public int CurrentRewardTotalInputTimes = 0;
        public Action<int> OnInputCountChange;
        public Action<int> OnGoldCountChange;

        private int inputCount = 0;
        public int InputCount {
            get => inputCount;
            set {
                inputCount = value;
                CurrentRewardInputTimes++;
                OnInputCountChange?.Invoke(value);
                AddExp();
                AddGold();
            }
        }

        private int coinCount = 0;
        public int CoinCount {
            get => coinCount;
            set {
                coinCount = value;
                OnGoldCountChange?.Invoke(value);
            }
        }

        public PetController() {
            InitInputRewardMap();
        }

        private void InitInputRewardMap() {
            var keys = InputRewardConfig.GetKeys();
            for (int i = 0; i < keys.Count; i++) {
                var key = keys[i];
                var config = InputRewardConfig.Get(key);
                var count = config.count;
                inputTimesList.Add(count);
                if (currentRewardIndex == -1 && CurrentRewardInputTimes < count) {
                    currentRewardIndex = config.id;
                    CurrentRewardTotalInputTimes = count;
                }
            }
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
            if (CurrentRewardInputTimes % CurrentRewardTotalInputTimes == 0) {
                var config = InputRewardConfig.Get(currentRewardIndex);
                int stageBonus = Random.Range(config.min, config.max);
                AddCoin(stageBonus);
                Debug.Log("达到输入里程碑，额外奖励：" + stageBonus);
                if (CurrentRewardInputTimes >= GetMaxInputCount()) {
                    // 重复奖励阶段
                    currentRewardIndex = 1;
                    CurrentRewardInputTimes = 0;
                } else {
                    currentRewardIndex++;
                }

                config = InputRewardConfig.Get(currentRewardIndex);
                CurrentRewardTotalInputTimes = config.count;
            }
        }

        private void AddCoin(int count) {
            CoinCount += count;
        }

        private int GetMaxInputCount() {
            if (inputTimesList.Count > 0) {
                return inputTimesList[inputTimesList.Count - 1];
            }

            return 0;
        }

        private void AddExp() {
            ControllerManager.Instance.Get<SettingController>().AddCurrentPetExp();
        }
    }
}