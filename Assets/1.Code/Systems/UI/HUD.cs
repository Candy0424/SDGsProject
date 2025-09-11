using System;
using _1.Code.Defines;
using _1.Code.Systems.Turn;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _1.Code.Systems.UI
{
    public abstract class HUD : MonoBehaviour, ITurnAble
    {
        [Header("HUDSettings")]
        [field: SerializeField] public HUDTypes HudAttribute { get; private set; }
        [SerializeField] private float maxValue = 100f;
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float duration = 0.5f;

        [Space, Header("DeltaSettings")]
        [SerializeField] private float minMinusValue = 1f;
        [SerializeField] private float maxMinusValue = 10f;
        [SerializeField] protected float valueMultiplier = 1f;
        
        
        private Slider _hudGage;

        public UnityEvent OnZeroEvent;
        
        private float HUDValue
        {
            get => _hudGage.value;
            set
            {
                float v = Mathf.Clamp(value, minValue, maxValue);
                DOVirtual.Float(_hudGage.value, v, duration, x => _hudGage.value = x).SetEase(Ease.OutSine);
            }
        }

        public void GageCalc(int value) => HUDValue += value;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Start()
        {
            // 나중에 필요하면 써야디
        }

        public void Initialize()
        {
            _hudGage = GetComponent<Slider>();
            Debug.Assert(_hudGage != null, $"This GameObject have not Slider Component!");
            AfterInit();
        }
        
        public virtual void AfterInit()
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
            
        public void TurnInit()
        {
            float value = (Random.Range(minMinusValue, maxMinusValue)) * valueMultiplier;
            HUDValue -= value;
        }

        public void TurnEnd()
        {
        }

        public void HUDDeltaCalc(float value) => valueMultiplier += value;
    }
}