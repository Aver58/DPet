using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;

using Kirurobo;
using Scripts.Bussiness.GamePlay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingView : UIViewBase{
    public Toggle isWindowsTop;
    public Toggle isClickThrough;
    public Dropdown languageDropdown;
    public Button BtnClose;

    private UniWindowController uniWindowController;
    protected override void OnInit() {
        uniWindowController = UniWindowController.current;
        BtnClose.onClick.AddListener(() => {
            ControllerManager.Instance.Close<SettingController>();
        });

        InitSettingPanel();
    }

    protected override void OnClear() { }
    protected override void OnOpen() { }
    protected override void OnClose() { }

    private void InitSettingPanel() {
        isWindowsTop.isOn = PlayerPrefs.GetInt(PlayerPrefsManager.IsWindowsTopMost, 0) == 1;
        isWindowsTop.onValueChanged.AddListener(OnToggleIsTopChanged);
        isClickThrough.isOn = PlayerPrefs.GetInt(PlayerPrefsManager.IsClickThrough, 0) == 1;
        isClickThrough.onValueChanged.AddListener(OnToggleClickThroughChanged);

        languageDropdown.captionText.text = LocalizationFeature.GetLanguageName(LocalizationFeature.CurrentLanguage);
        languageDropdown.ClearOptions();
        languageDropdown.options.Add(new Dropdown.OptionData {text = "中文"});
        languageDropdown.options.Add(new Dropdown.OptionData {text="English"});
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    private void OnToggleIsTopChanged(bool isOn) {
        uniWindowController.isTopmost = isOn;
        PlayerPrefs.SetInt(PlayerPrefsManager.IsWindowsTopMost, isOn ? 1 : 0);
    }

    private void OnToggleClickThroughChanged(bool isOn) {
        uniWindowController.isClickThrough = isOn;
        PlayerPrefs.SetInt(PlayerPrefsManager.IsClickThrough, isOn ? 1 : 0);
    }

    private void OnLanguageChanged(int index) {
        switch (index) {
            case 0:
                LocalizationFeature.CurrentLanguage = SystemLanguage.Chinese;
                break;
            case 1:
                LocalizationFeature.CurrentLanguage = SystemLanguage.English;
                break;
        }

        LocalizationFeature.SetCurrentLanguage(LocalizationFeature.CurrentLanguage);
    }
}
