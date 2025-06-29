using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;

using Kirurobo;
using UnityEngine.UI;

public class SettingView : UIViewBase {
    public InventoryPanel InventoryPanel;
    public ShopPanel ShopPanel;
    public SettingPanel SettingPanel;
    public Button BtnInventory;
    public Button BtnShop;
    public Button BtnSetting;
    public Button BtnClose;

    private UniWindowController uniWindowController;
    protected override void OnInit() {
        uniWindowController = UniWindowController.current;
        BtnClose.onClick.AddListener(() => {
            ControllerManager.Instance.Close<SettingController>();
        });
        BtnInventory.onClick.AddListener(() => {
            InventoryPanel.gameObject.SetActive(true);
        });
        BtnShop.onClick.AddListener(() => {
            ShopPanel.gameObject.SetActive(true);
        });
        BtnSetting.onClick.AddListener(() => {
            SettingPanel.gameObject.SetActive(true);
        });
    }

    protected override void OnClear() { }

    protected override void OnOpen() {
        InventoryPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        SettingPanel.gameObject.SetActive(false);
        BtnInventory.onClick.Invoke();
    }
    
    protected override void OnClose() { }
}
