using UnityEngine;

namespace Assets.Scripts.Test
{
    public class TestComponentOnDestroy : MonoBehaviour
    {
        [SerializeField]
        private ComponentWithOnDestroy _component;

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Destroy Item"))
            {
                Object.Destroy(_component.gameObject);
            }
        }
    }
}