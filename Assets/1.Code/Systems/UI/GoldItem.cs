using System;
using _1.Code.Defines;
using _1.Code.Systems.Turn;
using TMPro;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class GoldItem : MonoBehaviour, ITurnAble
    {
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private int currentGold;
        [SerializeField] private int maxGold;
        [SerializeField] private int getGoldValue;
        
        private GoldAmount _goldAmount;

        private void Awake()
        {
            _goldAmount = GetComponent<GoldAmount>();
        }

        private void Start()
        {
            UpdateGold();
        }

        public void TurnInit()
        {
            GetGold();
            UpdateGold();
        }

        private void GetGold()
        {
            float value = _goldAmount.CurrentGoldAmount / 100f;
            currentGold += Mathf.CeilToInt(getGoldValue * value);
        }

        public void GoldCalc(int gold) => currentGold += gold;

        public void UpdateGold()
        {
            goldText.text = currentGold.ToString() + "$";
        }

        public void TurnEnd()
        {
        }
    }
}