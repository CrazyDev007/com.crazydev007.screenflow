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
            ApplySafeArea(screen);
            _root.Add(screen);
            SetupScreen(screen);
        }
        
        public void SetupRoot(VisualElement root)
        {
            this._root = root;
        }
        
        private void ApplySafeArea(VisualElement screen)
        {
            // Calculate top space (notch area)
            var topSpace = Screen.height - Screen.safeArea.yMax;
            var bottomSpace = Screen.safeArea.yMin;

            // Apply as padding to avoid the notch
            screen.style.paddingTop = topSpace;
            screen.style.paddingBottom = bottomSpace;
        }

        protected abstract void SetupScreen(VisualElement screen);
    }

}