using System.Collections.Generic;
using Scripts.Bussiness.Controller;
using UnityEngine;

public class InventoryPanel : ABSPanelBase {
    public InventoryItem InventoryItemPrefab;
    public Transform ContentTransform;
    private SettingController controller;
    private int rowCount = 6;
    private int columnCount = 6;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>(36);

    protected override void OnInit() {
        controller = ControllerManager.Instance.Get<SettingController>();
        if (controller == null) {
            Debug.LogError("SettingController is not initialized.");
            return;
        }

        InitAllInventoryItemsGrid();
    }

    protected override  void OnClear() { }

    protected override void OnOpen() {
        base.OnOpen();

        InitAllInventoryItems();
    }

    protected override void OnClose() {
        base.OnClose();
    }

    private void InitAllInventoryItemsGrid() {
        for (int i = 0; i < rowCount; i++) {
            for (int j = 0; j < columnCount; j++) {
                var go = Instantiate(InventoryItemPrefab, ContentTransform);
                var inventoryItem = go.GetComponent<InventoryItem>();
                inventoryItems.Add(inventoryItem);
            }
        }
    }

    private void InitAllInventoryItems() {
        var items = controller.GetInventoryItems();
        for (int i = 0; i < items.Count; i++) {
            var item = items[i];
            // todo 上限设置
            var inventoryItem = inventoryItems[i];
            inventoryItem.Init(item.Id);
        }
    }

    // todo 图鉴系统
}