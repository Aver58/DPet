using UnityEngine;

public class ShopPanel : ABSPanelBase {
    public GameObject ShopItem;
    public Transform ShopContent;

    protected override void OnInit() {
        InitShopItem();
    }

    protected override  void OnClear() {
    }

    protected override void OnOpen() {
        base.OnOpen();
    }

    protected override void OnClose() {
        base.OnClose();
    }

    private void InitShopItem() {
        var keys = ShopTableConfig.GetKeys();
        for (int i = 0; i < keys.Count; i++) {
            var key = keys[i];
            var config = ShopTableConfig.Get(key);
            var go = Instantiate(ShopItem, ShopContent);
            var shopItem = go.GetComponent<ShopItem>();
            if (shopItem != null) {
                shopItem.Init(config);
            }
        }
    }
}