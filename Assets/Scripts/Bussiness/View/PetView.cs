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

    private int inputCount = 0;
    private PetController petController;
    private float scrollValue = 0f;
    private UniWindowController uniWindowController;

    private int InputCount {
        get {
            return inputCount;
        } set {
            inputCount = value;
            OnInputCountChange();
        }
    }

    private void OnInputCountChange() {
        TxtInputCount.text = inputCount.ToString();

    }

    protected override void OnInit() {
        petController = Controller as PetController;
        InitSetting();
        InitMainPet();
        GlobalKeyHook.Instance.OnKeyPressed += (keyDownCount) => {
            InputCount++;
        };

        uiMoveObjMono.OnPointerDownAction = OnPointerDown;
    }

    private void InitMainPet() {
        var petId = PlayerPrefs.GetInt(PlayerPrefManager.MainPetId, 1);
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
        uniWindowController.isTopmost = PlayerPrefs.GetInt(PlayerPrefManager.IsWindowsTopMost, 0) == 1;
        uniWindowController.isClickThrough = PlayerPrefs.GetInt(PlayerPrefManager.IsClickThrough, 0) == 1;
        uniWindowController.isTransparent = true;
        uniWindowController.alphaValue = 1;
    }

    protected override void OnClear() {
        base.OnClear();
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
}
