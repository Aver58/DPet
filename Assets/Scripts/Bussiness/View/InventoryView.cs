using Scripts.Bussiness.Controller;
using Scripts.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : UIViewBase {
    [SerializeField] private Button BtnClose;
    [SerializeField] private Transform Content;
    [SerializeField] private GameObject InventoryItemPrefab;

    private int ColumnCount = 6;
    private int MinRowCount = 6;

    protected override void OnInit() {
        BtnClose.onClick.AddListener(() => {
            ControllerManager.Instance.Close<SettingController>();
        });

        InitInventory();
    }

    protected override void OnClear() { }
    protected override void OnOpen() { }
    protected override void OnClose() { }
    protected override void UpdateView() {}

    private void InitInventory() {

    }
}
