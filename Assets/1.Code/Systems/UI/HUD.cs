using System;
using _1.Code.Defines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1.Code.Systems.UI
{
    public abstract class HUD : MonoBehaviour
    {
        [SerializeField] private UiHuDs hudAttribute;
        [SerializeField] private float maxValue;
        [SerializeField] private float minValue;
        [SerializeField] protected float valueMultiplier;
        
        private Slider _hudGage;

        public UnityEvent OnZeroEvent;

        protected virtual void Awake()
        {
            Initialize();
            AfterInit();
        }

        private void Initialize()
        {
            _hudGage = GetComponent<Slider>();
            Debug.Assert(_hudGage != null, $"This GameObject have not Slider Component!");
        }
        
        protected virtual void AfterInit()
        {
            OnZeroEvent.AddListener(HandleZeroEvent);
            
            _hudGage.maxValue = maxValue;
            _hudGage.minValue = minValue;
            _hudGage.value = maxValue;
        }

        private void OnDestroy()
        {
            OnZeroEvent.RemoveListener(HandleZeroEvent);
        }

        protected virtual void ZeroInvoke()
        {
            OnZeroEvent?.Invoke();
        }
        
        protected abstract void HandleZeroEvent();
    }
}