using System.Collections.Generic;
using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;

using UnityEngine.UI;

public class SettingView : UIViewBase {
    public InventoryPanel InventoryPanel;
    public ShopPanel ShopPanel;
    public SettingPanel SettingPanel;
    public Button BtnInventory;
    public Button BtnShop;
    public Button BtnSetting;
    public Button BtnClose;
    private List<ABSPanelBase> panelList;
    private int lastSelectIndex = 0;
    private SettingController SettingController => Controller as SettingController;

    protected override void OnInit() {
        BtnClose.onClick.AddListener(() => { ControllerManager.Instance.Close<SettingController>(); });
        panelList = new List<ABSPanelBase>() { InventoryPanel,ShopPanel,SettingPanel };
        BtnInventory.onClick.AddListener(() => { OnClickTab((int)TabIndex.Inventory); });
        BtnShop.onClick.AddListener(() => { OnClickTab((int)TabIndex.Shop); });
        BtnSetting.onClick.AddListener(() => { OnClickTab((int)TabIndex.Setting); });
    }

    protected override void OnClear() { }

    protected override void OnOpen() {
        InventoryPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        SettingPanel.gameObject.SetActive(false);
        BtnInventory.onClick.Invoke();
        lastSelectIndex = SettingController.LastSelectIndex;
        if (lastSelectIndex != 0) {
            OnClickTab(lastSelectIndex);
        }
    }
    
    protected override void OnClose() { }

    public void OnClickTab(int selectIndex) {
        var panel = panelList[lastSelectIndex];
        if (panel != null) {
            panel.Close();
        }

        lastSelectIndex = selectIndex;
        SettingController.LastSelectIndex = selectIndex;
        panel = panelList[selectIndex];
        if (panel != null) {
            panel.Init();
            panel.Open();
        }
    }

    public enum TabIndex {
        Inventory = 0,
        Shop = 1,
        Setting = 2
    }
}
