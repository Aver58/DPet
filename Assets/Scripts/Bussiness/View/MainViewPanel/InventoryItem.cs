using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
    public Image ImgQuality;
    public Image ImgIcon;

    // todo 拖拽合成，随机升级，升级到最高品质，可以进阶
    public void Init(int petId) {
        var config = PetMapConfig.Get(petId);
        if (config == null) {
            Debug.LogError($"Pet config not found for id: {petId}");
            return;
        }

        ImgQuality.SetSprite($"QualityBorder{config.quality + 1}");
        ImgIcon.SetSprite(config.sprite1, true);
        ImgIcon.enabled = true;
    }
}