using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Framework.UI;

public class InventoryItem : MonoBehaviour {
    public Image ImgQuality;
    public Image ImgIcon;

    // todo 拖拽合成，随机升级，进阶
    public void Init(int petId) {
        var config = PetMapConfig.Get(petId);
        if (config == null) {
            Debug.LogError($"Pet config not found for id: {petId}");
            return;
        }

        // ImgQuality.sprite = config.qualitySprite;
        ImgIcon.SetSprite(config.sprite1);
    }
}