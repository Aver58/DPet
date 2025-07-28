using System;
using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
    private int petId;
    private InventoryItemData data;
    private Action<InventoryItemData> clickCallBack;
    public Image ImgQuality;
    public Image ImgIcon;
    public Button BtnClick;

    // todo 拖拽合成，随机升级，升级到最高品质，可以进阶
    public void Init(InventoryItemData data) {
        EventManager.Instance.Register<InventoryItemData>(EventConstantId.OnLevelUpCurrentPet, OnLevelUpCurrentPet);

        petId = data.PetId;
        BtnClick.onClick.AddListener(() => {
            clickCallBack?.Invoke(data);
        });

        var config = PetMapConfig.Get(petId);
        if (config == null) {
            Debug.LogError($"Pet config not found for id: {petId}");
            return;
        }

        ImgQuality.SetSprite($"QualityBorder{config.quality + 1}");
        OnLevelUpCurrentPet(data);
        ImgIcon.enabled = true;
    }

    public void Clear() {
        EventManager.Instance.Unregister<InventoryItemData>(EventConstantId.OnLevelUpCurrentPet, OnLevelUpCurrentPet);
    }

    private void OnLevelUpCurrentPet(InventoryItemData data) {
        this.data = data;
        petId = this.data.PetId;
        var config = PetMapConfig.Get(petId);
        ImgIcon.SetSprite(config.sprite);
    }

    public void SetClickCallBack(Action<InventoryItemData> callBack) {
        clickCallBack = callBack;
    }
}