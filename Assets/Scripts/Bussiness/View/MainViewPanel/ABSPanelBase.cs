using Scripts.Framework.UI;
using UnityEngine;

public abstract class ABSPanelBase : MonoBehaviour {
    private bool IsInit;
    public virtual void Init() {
        if (!IsInit) {
            OnInit();
            IsInit = true;
        }
    }
    protected virtual void OnInit() { }
    protected virtual void OnClear(){ }

    public void Open() {
        OnOpen();
    }

    public void Close() {
        OnClose();
    }

    protected virtual void OnOpen() {
        gameObject.SetActive(true);
    }

    protected virtual void OnClose() {
        gameObject.SetActive(false);
    }
}