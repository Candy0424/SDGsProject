using System;
using _1.Code.Systems.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _1.Code.Systems.PolicyCard
{
    public class PolicyCardItem : MonoBehaviour, ISelectAble
    {
        [field : SerializeField] public PolicyCardInfo CardInfo { get; private set; }
        [SerializeField] private Image cardImage;

        private IPolicyCard _card;

        public PolicyCardItem Initialize(PolicyCardInfo info, float size)
        {
            CardInfo = info;
            cardImage.sprite = CardInfo.cardImage;
            _card = GetComponentInParent<IPolicyCard>();
            transform.localScale = new Vector3(size, size, size);
            return this;
        }
        
        public void Select()
        {
            _card.SetPolicyCard(CardInfo);
        }
    }
}