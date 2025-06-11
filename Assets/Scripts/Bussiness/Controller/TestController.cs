using Scripts.Bussiness.Model;
using Scripts.Framework.UI;
using UnityEngine;

namespace Scripts.Bussiness.Controller {
    public class TestController : ControllerBase {
        public new TestViewModel Model { get; private set; }

        public TestController() {
            Model = new TestViewModel { Health = 100 };
            Model.PropertyChanged += (sender, args) => NotifyModelChanged();
        }

        public void TakeDamage(int damage) {
            Model.Health -= damage;
            // 业务逻辑（如死亡检测）
            if (Model.Health <= 0) {
                Debug.Log("Player Died!");
            }
        }
    }
}