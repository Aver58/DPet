using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

public class TestView : UIViewBase {
    [SerializeField] private Button _testBtn;
    [SerializeField] private Text _testText;

    protected override void OnInit() {
        _testBtn.onClick.AddListener(OnTakeDamageButtonClick);
    }

    protected override void OnClear() { }
    protected override void OnOpen() { }
    protected override void OnClose() { }

    protected override void UpdateView() {
        if (Controller is TestController testViewController) {
            _testText.text = $"Health: {testViewController.Model.Health}";
        }
    }

    private void OnTakeDamageButtonClick() {
        (Controller as TestController)?.TakeDamage(10);
    }
}
