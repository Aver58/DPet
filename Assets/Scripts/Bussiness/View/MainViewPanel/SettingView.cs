using System.Collections.Generic;
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
    private List<ABSPanelBase> panelList;
    private int lastSelectIndex = 0;

    private UniWindowController uniWindowController;
    protected override void OnInit() {
        uniWindowController = UniWindowController.current;
        BtnClose.onClick.AddListener(() => { ControllerManager.Instance.Close<SettingController>(); });
        panelList = new List<ABSPanelBase>() { InventoryPanel,ShopPanel,SettingPanel };
        BtnInventory.onClick.AddListener(() => { OnClickTab(0); });
        BtnShop.onClick.AddListener(() => { OnClickTab(1); });
        BtnSetting.onClick.AddListener(() => { OnClickTab(2); });
    }

    protected override void OnClear() { }

    protected override void OnOpen() {
        InventoryPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        SettingPanel.gameObject.SetActive(false);
        BtnInventory.onClick.Invoke();
    }
    
    protected override void OnClose() { }

    private void OnClickTab(int selectIndex) {
        var panel = panelList[lastSelectIndex];
        if (panel != null) {
            panel.Close();
        }

        lastSelectIndex = selectIndex;
        panel = panelList[selectIndex];
        if (panel != null) {
            panel.Init();
            panel.Open();
        }
    }
}
