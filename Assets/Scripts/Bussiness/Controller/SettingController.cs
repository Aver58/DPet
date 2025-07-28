using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class SettingController : ControllerBase {
        private int autoId = 0;
        public int LastSelectIndex { get; set; }
        private List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
        private int defaultPetId = 1;
        private InventoryItemData CurrentInventoryItemData;

        public SettingController() {
            InitAllInventoryItem();
        }

        public void OnClickTab(SettingView.TabIndex selectIndex) {
            LastSelectIndex = (int)selectIndex;
            if (IsOpen) {
                if (Window != null && Window is SettingView settingView) {
                    settingView.OnClickTab((int)selectIndex);
                }
            } else {
                OpenAsync();
            }
        }

        #region Inventory

        private void InitAllInventoryItem() {
            // 初始化仓库数据，添加默认仓库宠物
            if (inventoryItemDatas.Count == 0) {
                CurrentInventoryItemData = AddInventoryItem(defaultPetId);
            }
        }

        public List<InventoryItemData> GetAllInventoryItems() {
            return inventoryItemDatas;
        }

        private InventoryItemData AddInventoryItem(int petId) {
            var data = new InventoryItemData {
                Id = autoId,
                PetId = petId,
                Stage = 1, // 默认阶段为1
            };

            autoId++;
            inventoryItemDatas.Add(data);
            return data;
        }

        public void AddCurrentPetExp() {
            var data = CurrentInventoryItemData;
            data.Exp++;
            // 检查是否可以升级
            if (data.Exp >= GetExpToLevelUp(data.PetId)) {
                LevelUpCurrentPet();
            }

            EventManager.Instance.Dispatch(EventConstantId.OnInventoryItemDataChange, CurrentInventoryItemData);
        }

        private int GetExpToLevelUp(int petId) {
            var config = PetMapConfig.Get(petId);
            return config.exp;
        }

        private void LevelUpCurrentPet() {
            var data = CurrentInventoryItemData;
            var config = PetMapConfig.Get(data.PetId);
            if (config.levelUpId != 0) {
                data.PetId = config.levelUpId;
                data.Exp = 0;
                EventManager.Instance.Dispatch(EventConstantId.OnLevelUpCurrentPet, data);
            }
        }

        #endregion

        #region Shop

        public void BuyShopItem(int shopId) {
            // 开包 todo 开包动画
            // 仓库新增
            var config = ShopTableConfig.Get(shopId);
            if (config == null) {
                Debug.LogError($"Shop config not found for id: {shopId}");
                return;
            }

            var goldCount = ControllerManager.Instance.Get<PetController>().CoinCount;
            if (goldCount < config.price) {
                // todo tip
                Debug.LogError("Not enough input count to buy the item.");
                return;
            }

            var nProbability = config.nProbability;
            var rProbability = config.rProbability;
            var sRProbability = config.sRProbability;
            var sSRProbability = config.sSRProbability;
            // 按配置概率随机出获得的是什么品质
            QualityDefine quality = QualityDefine.Normal;
            int randomValue = Random.Range(0, 100);
            if (randomValue < nProbability) {
                quality = QualityDefine.Normal;
            } else if (randomValue < nProbability + rProbability) {
                quality = QualityDefine.Rare;
            } else if (randomValue < nProbability + rProbability + sRProbability) {
                quality = QualityDefine.SuperRare;
            } else {
                quality = QualityDefine.SuperSuperRare;
            }

            var petPool = PetMapConfig.GetPetPool((int)quality);
            if (petPool.Count == 0) {
                Debug.LogError($"No pets available for quality: {quality}");
                return;
            }

            int randomIndex = Random.Range(0, petPool.Count);
            int petId = petPool[randomIndex];
            Debug.Log($"Bought {shopId}, quality: {quality}, petId: {petId}");
            AddInventoryItem(petId);
            ControllerManager.Instance.Get<PetController>().CoinCount -= config.price;
        }

        #endregion
    }

    public class InventoryItemData {
        public int Id;
        public int PetId;   // 宠物ID
        public int Stage;   // 进化阶段
        public int Exp;     // 经验值 点击次数
    }

    public enum QualityDefine {
        Normal,         //N
        Rare,           //R
        SuperRare,      //SR
        SuperSuperRare, //SSR
    }
}