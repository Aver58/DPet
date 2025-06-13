using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Bussiness.GamePlay {
    // UI 移动组件，挂载在UI元素上，该对象就可以被拖动
    public class UIMoveObjMono : MonoBehaviour {
        private RectTransform rectTransform;
        public Action<BaseEventData> OnPointerDownAction;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();

            var image = gameObject.AddComponent<Image>();
            image.color = Color.clear;

            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            entry.callback.AddListener(data => { OnDrag((PointerEventData)data); });
            eventTrigger.triggers.Add(entry);

            var entry1 = new EventTrigger.Entry();
            entry1.eventID = EventTriggerType.PointerDown;
            entry1.callback.AddListener(data => { OnPointerDown((PointerEventData)data); });
            eventTrigger.triggers.Add(entry1);
        }

        private void OnPointerDown(BaseEventData eventData) {
            OnPointerDownAction?.Invoke(eventData);
        }

        private void OnDrag(PointerEventData eventData) {
            Vector3 worldPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out worldPosition
            );

            rectTransform.position = worldPosition;
        }
    }
}