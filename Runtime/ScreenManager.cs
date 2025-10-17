using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ScreenFlow
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance { get; private set; }
        [SerializeField] private List<UIScreenMapping> screenMappings;

        private readonly Dictionary<string, ScreenUI> _screens = new Dictionary<string, ScreenUI>();

        private VisualElement _root;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _root = GetComponent<UIDocument>().rootVisualElement;
            foreach (var mapping in screenMappings)
            {
                if (mapping.screen != null && !_screens.ContainsKey(mapping.type))
                {
                    _screens.Add(mapping.type, mapping.screen);
                    mapping.screen.SetupRoot(_root);
                    if (mapping.isDefault)
                    {
                        ShowScreen(mapping.type);
                    }
                }
            }
        }

        public void ShowScreen(string screenType)
        {
            if (_screens.TryGetValue(screenType, out ScreenUI screen))
            {
                screen.Show();
            }
            else
            {
                Debug.LogError($"Screen of type '{screenType}' not found. Available screens: {string.Join(", ", _screens.Keys)}");
            }
        }

        public IEnumerable<string> GetAvailableScreenTypes()
        {
            return _screens.Keys;
        }
    }

    [Serializable]
    public struct UIScreenMapping
    {
        public string type;
        public ScreenUI screen;
        public bool isDefault;
    }
}