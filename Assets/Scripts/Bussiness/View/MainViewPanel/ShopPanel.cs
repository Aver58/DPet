using Scripts.Bussiness.Controller;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : ABSPanelBase {
    public GameObject ShopItem;
    public Transform ShopContent;
    public Text TxtGold;
    private PetController petController;

    protected override void OnInit() {
        InitShopItem();
        petController = ControllerManager.Instance.Get<PetController>();
    }

    protected override  void OnClear() {
    }

    protected override void OnOpen() {
        base.OnOpen();
        petController.OnGoldCountChange += OnGoldCountChange;
        OnGoldCountChange(petController.CoinCount);
    }

    protected override void OnClose() {
        petController.OnGoldCountChange -= OnGoldCountChange;
        base.OnClose();
    }

    private void OnGoldCountChange(int count) {
        TxtGold.text = count.ToString();
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