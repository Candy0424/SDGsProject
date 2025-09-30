using System;
using System.Collections.Generic;
using _1.Code.Defines;
using _1.Code.Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1.Code.Systems.PolicyCard
{
    public class PolicyCardItem : MonoBehaviour, ISelectAble
    {
        [field : SerializeField] public PolicyCardInfo CardInfo { get; private set; }
        [SerializeField] private Image cardImage;
        [SerializeField] private TextMeshProUGUI descriptionText, deltaValueText, goldText;

        private IPolicyCard _card;
        private bool _isEquip;

        public Action<PolicyCardItem> OnBuyEvent;
        public Action<PolicyCardItem> OnUseEvent;

        public void Start()
        {
            _card = GetComponentInParent<IPolicyCard>();
        }
        public void Initialize(PolicyCardInfo info, float size, bool isEquip = false)
        {
            EventClear();
            CardInfo = info;
            cardImage.sprite = CardInfo.cardImage;
            transform.localScale = new Vector3(size, size, size);
            descriptionText.text = info.cardDescription;
            _isEquip = isEquip;
            string str = "";
            List<HUDDatas> l = info.datas; 
            foreach (HUDDatas data in l)
            {
                str += TypeToString(data.valueType) + "   ";
            }
            str += "\n";
            foreach (HUDDatas data in l)
            {
                string v = data.value.ToString();
                str += v + "   ";
            }
            deltaValueText.text = str;
            goldText.text = info.gold.ToString() + '$';
        }

        private void EventClear()
        {
            OnBuyEvent = null;
            OnUseEvent = null;
        }

        public void Equip() => _isEquip = true;

        public void HandleInteract()
        {
            if (_isEquip)
            {
                OnUseEvent?.Invoke(this);
            }
            else
            {
                OnBuyEvent?.Invoke(this);
            }
        }
        
        public void Select()
        {
            _card.SetPolicyCard(CardInfo);
        }

        private string TypeToString(HUDTypes type)
        {
            switch (type)
            {
                case HUDTypes.Co2:
                    return "탄소";
                case HUDTypes.Forest:
                    return "산림";
                case HUDTypes.IceProtection:
                    return "빙하";
                case HUDTypes.OceanHealth:
                    return "해양";
                case HUDTypes.Gold:
                    return "예산";
                
                case HUDTypes.None:
                default:
                    return null;
            }
        }
    }
}