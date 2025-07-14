using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class SettingController : ControllerBase {
        // 仓库数据
        // private List<> Inventory

        public SettingController() {
            InitAllInventoryItem();
        }

        private void InitAllInventoryItem() {
            // 初始化仓库数据，添加默认仓库宠物

            // InventoryItemData[] allItems = InventoryTableConfig.GetAllItems();


        }

        public void BuyShopItem(int shopId) {
            // 开包 todo 开包动画
            // 仓库新增
            var config = ShopTableConfig.Get(shopId);
            if (config == null) {
                Debug.LogError($"Shop config not found for id: {shopId}");
                return;
            }

            var inputCount = ControllerManager.Instance.Get<PetController>().InputCount;
            if (inputCount < config.price) {
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

        }

        private void AddInventoryItem(int petId) {

        }
    }

    public struct InventoryItemData {
        public int Id;
        public int Count;   // 数量
        public int Level;   // 等级
        public float ClickAdd;// 点击效率
    }

    public enum QualityDefine {
        Normal,//N
        Rare,//R
        SuperRare,//SR
        SuperSuperRare,//SSR
    }
}