using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Scripts.Framework.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Scripts.Bussiness.Controller;

using Kirurobo;
using Scripts.Bussiness.GamePlay;

public class PetView : UIViewBase {
    public Image ImgMain;
    public Image ImgLeft;
    public Image ImgRight;
    public Image ImgHead;
    public UIMoveObjMono uiMoveObjMono;
    public Text TxtInputCount;
    public Button BtnChest;

    private PetController petController;
    private UniWindowController uniWindowController;

    protected override void OnInit() {
        petController = Controller as PetController;
        if (petController == null) {
            Debug.LogError("PetController is not initialized.");
            return;
        }
        petController.OnInputCountChange += OnInputCountChange;
        uiMoveObjMono.OnPointerDownAction = OnPointerDown;
        GlobalKeyHook.Instance.OnKeyPressed += OnKeyPressed;
        BtnChest.onClick.AddListener(OnBtnChest);
        InitSetting();
        InitMainPet();
    }

    protected override void OnClear() {
        base.OnClear();
    }

    private void InitMainPet() {
        var petId = PlayerPrefs.GetInt(PlayerPrefsManager.MainPetId, 1);
        var config = PetMapConfig.Get(petId.ToString());
        if (config == null) {
            Debug.LogError($"Pet config not found for id: {petId}");
            return;
        }
        var petSprite = config.sprites[0];
        ImgMain.SetSprite(petSprite);
    }

    private void InitSetting() {
        uniWindowController = UniWindowController.current;
        uniWindowController.isTopmost = PlayerPrefs.GetInt(PlayerPrefsManager.IsWindowsTopMost, 0) == 1;
        uniWindowController.isClickThrough = PlayerPrefs.GetInt(PlayerPrefsManager.IsClickThrough, 0) == 1;
        uniWindowController.isTransparent = true;
        uniWindowController.alphaValue = 1;
    }


    private void OnPointerDown(BaseEventData eventData) {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData.button == PointerEventData.InputButton.Right){
            ControllerManager.Instance.OpenAsync<SettingController>();
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left){
            ControllerManager.Instance.Close<SettingController>();
        }
    }

    private void OnInputCountChange(int value) {
        TxtInputCount.text = petController.InputCount.ToString();
    }

    // 请求领取奖励
    private void OnBtnChest() {
        petController.GetAllReward();
    }

    private void OnKeyPressed(int keyDownCount) {
        petController.InputCount++;
        // tween动画,模拟果冻Q弹的动画
        LeanTween.

    }
}
