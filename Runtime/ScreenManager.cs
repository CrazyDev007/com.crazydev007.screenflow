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

        private readonly Dictionary<UIScreenType, IScreen> _screens = new Dictionary<UIScreenType, IScreen>();

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

        public void ShowScreen(UIScreenType screenType)
        {
            if (_screens.TryGetValue(screenType, out IScreen screen))
            {
                screen.Show();
            }
            else
            {
                Debug.LogError($"Screen of type {screenType} not found.");
            }
        }
    }

    [Serializable]
    public struct UIScreenMapping
    {
        public UIScreenType type;
        public IScreen screen;
        public bool isDefault;
    }


    public enum UIScreenType
    {
        Lobby,
        Setting,
        Gameplay,
        GameEnd, Login, Register, CreatePassword, ForgotPassword, EnterOtp, SentOtp, ChangePassword, Home,
    }
}