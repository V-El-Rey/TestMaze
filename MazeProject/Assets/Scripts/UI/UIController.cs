using System;
using Base;
using Struct;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UI
{
    internal class UIController : BaseController
    {
        private UIView _uiView;
        private readonly GameObject _rootUI;
        private TouchScreenKeyboard _keyboard;

        public UnityAction<MazeSettings> GetSettings;

        public UIController(UIView uiView, GameObject rootUI)
        {
            _uiView = uiView;
            _rootUI = rootUI;
        }

        public override void StartExecute()
        {
            base.StartExecute();
            Object.Instantiate(_uiView.uiPrefab, _rootUI.transform);
            _uiView = Object.FindObjectOfType<UIView>();
            _uiView.generateButton.onClick.AddListener(() => { GetSettings?.Invoke(GetMazeSettings()); });
            _uiView.heightInput.onSelect.AddListener((x) =>
            {
                _keyboard = TouchScreenKeyboard.Open(_uiView.heightInput.text, TouchScreenKeyboardType.NumberPad);
            });
            _uiView.widthInput.onSelect.AddListener((x) =>
            {
                _keyboard = TouchScreenKeyboard.Open(_uiView.widthInput.text, TouchScreenKeyboardType.NumberPad);
            });
        }

        private MazeSettings GetMazeSettings()
        {
            var result = new MazeSettings();
            
            if (_uiView.widthInput.text != "" && _uiView.heightInput.text != "")
            {
                var resultHeight = Convert.ToInt32(_uiView.heightInput.text);
                var resultWidth = Convert.ToInt32(_uiView.widthInput.text);
                if (resultHeight > 0 && resultWidth > 0)
                {
                    result.Height = resultHeight;
                    result.Width = resultWidth;
                    return result;
                }
            }
            else
            {
                return result;
            }
            return result;
            // Debug.Log($"Height/Width: {result.Height} : {result.Width}");
        }
    }
}