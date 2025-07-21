using UnityEngine;
using Debug = UnityEngine.Debug;
using Scripts.Framework.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Scripts.Bussiness.Controller;

using Kirurobo;
using Scripts.Bussiness.GamePlay;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PetView : UIViewBase {
    public Image ImgMain;
    public Image ImgLeft;
    public Image ImgRight;
    public Image ImgHead;
    public UIMoveObjMono uiMoveObjMono;
    public Text TxtInputCount;
    public Text TxtGiftCount;
    public Button BtnChest;
    public Slider SliderInputCount;

    private PetController petController;
    private UniWindowController uniWindowController;
    // 设置间隔时间
    private float animationInterval = 0.2f;

    protected override void OnInit() {
        petController = Controller as PetController;
        if (petController == null) {
            Debug.LogError("PetController is not initialized.");
            return;
        }
        petController.OnInputCountChange += OnInputCountChange;
        petController.OnGoldCountChange += OnGoldCountChange;
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
        var petSprite = config.sprite1;
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
        SliderInputCount.value = petController.InputCount;
    }

    private void OnGoldCountChange(int obj) {
        TxtGiftCount.text = petController.GoldCount.ToString();
    }

    // 打开商店界面
    private void OnBtnChest() {
        ControllerManager.Instance.Get<SettingController>().OnClickTab(SettingView.TabIndex.Shop);
    }

    private void OnKeyPressed(int keyDownCount) {
        petController.InputCount++;
        var imgMainTransform = ImgMain.transform;
        imgMainTransform.DOKill();
        imgMainTransform.localScale = Vector3.one;
        // tween动画,模拟果冻Q弹的动画
        var tweenScale = Random.Range(0.1f, 0.3f);
        imgMainTransform.DOPunchScale(new Vector3(tweenScale, tweenScale, 0), animationInterval, 10, 1);
    }
}
