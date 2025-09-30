using System;
using _1.Code.Defines;
using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.Turn;
using TMPro;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class GoldItem : MonoBehaviour, ITurnAble
    {
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI nextGoldText;
        [SerializeField] private int currentGold;
        [SerializeField] private int maxGold;
        [SerializeField] private int getGoldValue;
        
        private GoldAmount _goldAmount;

        private void Awake()
        {
            _goldAmount = GetComponent<GoldAmount>();

            _goldAmount.OnChangedValue += UpdateGold;
        }

        private void Start()
        {
            UpdateGold();
        }

        public void TurnInit()
        {
            GetGold();
        }

        private void GetGold()
        {
            GoldCalc(GetCalcGold());
        }
        
        private int GetCalcGold() => Mathf.CeilToInt((_goldAmount.CurrentGoldAmount / 100f) * getGoldValue);

        private void GoldCalc(int gold)
        { 
            currentGold += gold;
            UpdateGold();
        }

        private void UpdateGold()
        {
            goldText.text = "예산 : " + currentGold.ToString() + "$";
            int value = GetCalcGold();
            nextGoldText.text = "다음 예산 추정 : " + value.ToString() + "$"; 
        }

        public void TurnEnd()
        {
        }

        public bool CanBuyGoldCheck(PolicyCardInfo itemCardInfo)
        {
            if (itemCardInfo == null)
            {
                Debug.Log($"CardInfo Is Null : {itemCardInfo}");
                return false;
            }

            if (itemCardInfo.gold > currentGold)
            {
                return false;
            }

            int price = itemCardInfo.gold;
            
            GoldCalc(-price);
            return true;
        }
    }
}