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
    public Text TxtStage;
    public Text TxtCoinAdd;
    public Text TxtPrice;
    public Text TxtInputTimesToLevelUp;


    private SettingController controller;
    private int rowCount = 8;
    private int columnCount = 4;
    private List<InventoryItem> inventoryItems = new List<InventoryItem>(36);

    protected override void OnInit() {
        EventManager.Instance.Register<InventoryItemData>(EventConstantId.OnInventoryItemDataChange, OnInventoryItemDataChange);
        controller = ControllerManager.Instance.Get<SettingController>();
        if (controller == null) {
            Debug.LogError("SettingController is not initialized.");
            return;
        }

        InitAllInventoryItemsGrid();
    }

    protected override void OnClear() {
        EventManager.Instance.Unregister<InventoryItemData>(EventConstantId.OnInventoryItemDataChange, OnInventoryItemDataChange);
    }

    protected override void OnOpen() {
        base.OnOpen();

        InitAllInventoryItems();
        PetInfoPanel.gameObject.SetActive(false);
    }

    protected override void OnClose() {
        ClearAllInventoryItems();
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
        var inventoryItemDatas = controller.GetAllInventoryItems();
        Debug.Log($"InitAllInventoryItems count: {inventoryItemDatas.Count}");
        for (int i = 0; i < inventoryItemDatas.Count; i++) {
            var inventoryItemData = inventoryItemDatas[i];
            // todo 上限设置
            var inventoryItem = inventoryItems[i];
            inventoryItem.Init(inventoryItemData);
            inventoryItem.SetClickCallBack(OnClickInventoryItem);
        }
    }

    private void ClearAllInventoryItems() {
        var inventoryItemDatas = controller.GetAllInventoryItems();
        for (int i = 0; i < inventoryItemDatas.Count; i++) {
            var inventoryItem = inventoryItems[i];
            if (inventoryItem != null) {
                inventoryItem.Clear();
            }
        }
    }

    private InventoryItemData currentData;
    private void OnClickInventoryItem(InventoryItemData data) {
        Debug.Log($"Clicked inventory item with ID: {data.Id}, Pet ID: {data.PetId}");
        PetInfoPanel.gameObject.SetActive(true);
        OnInventoryItemDataChange(data);
    }

    private void OnInventoryItemDataChange(InventoryItemData data) {
        // if (data.Id != currentData?.Id) {
        //     Debug.Log($"data.Id {data.Id} currentData?.Id {currentData?.Id}");
        //     return;
        // }

        currentData = data;
        var petId = data.PetId;
        var config = PetMapConfig.Get(petId);
        if (config == null) {
            return;
        }

        TxtName.text = config.name;
        TxtQuality.text = $"品质：{((QualityDefine)config.quality).ToString()}";
        TxtStage.text = $"阶段：{data.Stage.ToString()}";
        var stage = data.Stage;
        var coinAdd = config.add;
        TxtCoinAdd.text = $"收益加成：+{coinAdd/100}%";
        TxtPrice.text = $"售价：{config.price.ToString()}";

        var levelUpText = "空";
        if (config.levelUpId == 0) {
            levelUpText = "已满级";
        } else {
            levelUpText = $"再点击{config.exp - data.Exp}次升级";
        }

        TxtInputTimesToLevelUp.text = levelUpText;
    }


    // todo 图鉴系统
}