using UnityEngine;
using UnityEngine.UIElements;

namespace ScreenFlow
{
    public abstract class ScreenUI : MonoBehaviour
    {
        private VisualElement _root;
        [SerializeField] protected VisualTreeAsset screenAsset;
        public virtual void Show()
        {
            _root.styleSheets.Clear();
            _root.Clear();
            VisualElement screen = screenAsset.CloneTree();
            screen.style.flexGrow = 1;
            _root.Add(screen);
            SetupScreen(screen);
        }
        
        public void SetupRoot(VisualElement root)
        {
            this._root = root;
        }

        protected abstract void SetupScreen(VisualElement screen);
    }

}