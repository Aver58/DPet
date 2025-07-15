using Scripts.Bussiness.Controller;
using UnityEngine;

public class InventoryPanel : ABSPanelBase {
    // private
    private SettingController controller;
    protected override void OnInit() {
        controller = ControllerManager.Instance.Get<SettingController>();
        if (controller == null) {
            Debug.LogError("SettingController is not initialized.");
            return;
        }

        InitAllInventoryItems();
    }

    protected override  void OnClear() { }

    protected override void OnOpen() {
        base.OnOpen();
    }

    protected override void OnClose() {
        base.OnClose();
    }

    private void InitAllInventoryItems() {
        var items = controller.GetInventoryItems();

    }
}