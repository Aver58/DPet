using System.Collections.Generic;
using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class SettingController : ControllerBase {
        public int LastSelectIndex { get; set; }
        private List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
        private int defaultPetId = 1;
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
                AddInventoryItem(defaultPetId);
            }
        }

        private void AddInventoryItem(int petId) {
            inventoryItemDatas.Add(new InventoryItemData {
                Id = petId,
            });
        }

        public List<InventoryItemData> GetInventoryItems() {
            return inventoryItemDatas;
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

            var goldCount = ControllerManager.Instance.Get<PetController>().GoldCount;
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
            // 随机出一个宠物
            if (petPool.Count == 0) {
                Debug.LogError($"No pets available for quality: {quality}");
                return;
            }

            int randomIndex = Random.Range(0, petPool.Count);
            int petId = petPool[randomIndex];
            Debug.Log($"Bought item with shopId: {shopId}, quality: {quality}, petId: {petId}");
            // 进入仓库
            AddInventoryItem(petId);
            ControllerManager.Instance.Get<PetController>().GoldCount -= config.price;
        }

        #endregion
    }

    public struct InventoryItemData {
        public int Id;
        public int Count;   // 数量
        public int Stage;   // 进化阶段
        public QualityDefine Quality; // 品质
        public float ClickAdd;// 收益加成
    }

    public enum QualityDefine {
        Normal,         //N
        Rare,           //R
        SuperRare,      //SR
        SuperSuperRare, //SSR
    }
}