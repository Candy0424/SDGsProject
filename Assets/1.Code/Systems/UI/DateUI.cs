using System;
using _1.Code.Systems.Turn;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _1.Code.Systems.UI
{
    public class DateUI : MonoBehaviour, ITurnAble
    {
        [SerializeField] private TextMeshProUGUI dayText;
        
        [SerializeField] private int currentDay = 1;
        [SerializeField] private int limitDay;

        public UnityEvent OnClearEvent;
        
        private void Start()
        {
            currentDay = 1;
            UpdateText();
        }

        public void TurnInit()
        {
            UpdateText();
            if (currentDay >= limitDay)
                OnClearEvent?.Invoke();
        }

        private void UpdateText()
        {
            dayText.text = "DAY : " + currentDay.ToString();
        }

        public void TurnEnd()
        {
            currentDay++;
        }
    }
}