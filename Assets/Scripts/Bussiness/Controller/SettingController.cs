using Scripts.Framework.UI;

namespace Scripts.Bussiness.Controller {
    public class SettingController : ControllerBase {
        // 仓库数据
        // private List<>

        public SettingController() { }

        public void BuyShopItem(int shopId) {
            // 开包 todo 开包动画

            // 仓库新增
            var config = ShopTableConfig.Get(shopId);

        }
    }

    public struct InventoryItemData {
        public int Id;
        public int Count;   // 数量
        public int Quality; // 品质
        public int Level;   // 等级
        public float clickEffect;// 点击效率
    }
}