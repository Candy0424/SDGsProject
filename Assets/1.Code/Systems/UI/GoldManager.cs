using System;
using _1.Code.Systems.Turn;
using TMPro;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class GoldManager : MonoBehaviour, ITurnAble
    {
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private int currentGold;
        [SerializeField] private int maxGold;

        private void Start()
        {
            UpdateGold();
        }

        public void TurnInit()
        {
            UpdateGold();
        }
        
        public void GoldCalc(int gold) => currentGold += gold;

        public void UpdateGold()
        {
            goldText.text = currentGold.ToString() + "원";
        }

        public void TurnEnd()
        {
        }
    }
}