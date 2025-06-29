using Kirurobo;
using Scripts.Bussiness.GamePlay;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour,IPanelBase {
    public Toggle isWindowsTop;
    public Toggle isClickThrough;
    public Dropdown languageDropdown;
    private UniWindowController uniWindowController;

    public void OnInit() {
        uniWindowController = UniWindowController.current;
        InitSettingPanel();
    }

    public void OnClear() {
    }

    public void OnOpen() {
    }

    public void OnClose() {
    }
    
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