using System;
using static Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _1.Code.Systems
{
    [CreateAssetMenu(fileName = "NewInputSystem", menuName = "SO/InputSystem", order = 0)]
    public class InputSystemSO : ScriptableObject, IPlayerActions
    {
        private Controls _controls;

        private Vector2 _mousePos;
        
        private void OnEnable()
        {
            _controls ??= new Controls();
            
            _controls.Enable();
            _controls.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void OnPos(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }

        public Vector2 GetMousePos() => _mousePos;
    }
}