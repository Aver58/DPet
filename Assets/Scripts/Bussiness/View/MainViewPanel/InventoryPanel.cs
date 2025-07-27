using System.Collections.Generic;
using Scripts.Bussiness.Controller;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : ABSPanelBase {
    public InventoryItem InventoryItemPrefab;
    public Transform ContentTransform;
    public Transform PetInfoPanel;
    public Text TxtName;
    public Text TxtQuality;
    public Text TxtCoinAdd;
    public Text TxtPrice;
    public Text TxtInputTimesToLevelUp;


    private SettingController controller;
    private int rowCount = 8;
    private int columnCount = 4;
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
        PetInfoPanel.gameObject.SetActive(false);
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
        Debug.Log($"InitAllInventoryItems count: {items.Count}");
        for (int i = 0; i < items.Count; i++) {
            var item = items[i];
            // todo 上限设置
            var inventoryItem = inventoryItems[i];
            inventoryItem.Init(item.Id);
        }
    }

    public void OnClickInventoryItem(int itemId) {
        Debug.Log($"Clicked inventory item with ID: {itemId}");
        PetInfoPanel.gameObject.SetActive(true);
        var config = PetMapConfig.Get(itemId);
        if (config == null) {
            return;
        }

        TxtName.text = config.name;
        TxtQuality.text = ((QualityDefine)config.quality).ToString();
    }


    // todo 图鉴系统
}