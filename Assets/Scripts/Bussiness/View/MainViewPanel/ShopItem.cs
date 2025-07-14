using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {
    public Button BtnBuy;
    public Image ImgIcon;
    public Text TxtDesc;
    public Text TxtPrice;

    private ShopTableConfig config;

    public void Init(ShopTableConfig config) {
        this.config = config;
        TxtDesc.text = $"{config.shopItemName}\r\nN:{config.nProbability}%\tR:{config.rProbability}%\tSR:{config.sRProbability}%\tSSR:{config.sSRProbability}%";
        TxtPrice.text = config.price.ToString();
        ImgIcon.SetSprite(config.shopIcon);
        BtnBuy.onClick.AddListener((() => {
            ControllerManager.Instance.Get<SettingController>().BuyShopItem(config.shopId);
        }));
    }
}