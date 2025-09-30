using System;
using _1.Code.Defines;
using TMPro;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class GoldAmount : MonoBehaviour
    {
        [SerializeField] private HUDTypes hudType;
        [SerializeField] private TextMeshProUGUI goldAmountText;
        [SerializeField] private int currentGoldAmount;
        [SerializeField] private int maxGoldAmount;

        public Action OnChangedValue;
        
        public int CurrentGoldAmount
        {
            get => currentGoldAmount;
            set
            {
                int v = value;
                if (v != currentGoldAmount)
                {
                    currentGoldAmount = v;
                    goldAmountText.text = "예산획득률 : " + v.ToString() + '%';
                    OnChangedValue?.Invoke();
                }
            }
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            CurrentGoldAmount = maxGoldAmount;
        }

        public void AmountCalc(int value) => CurrentGoldAmount += value;
    }
}