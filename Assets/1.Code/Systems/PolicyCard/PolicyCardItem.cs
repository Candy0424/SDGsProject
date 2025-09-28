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
        [field : SerializeField] public PolicyCardInfo cardInfo { get; private set; }
        [SerializeField] private Image cardImage;

        private IPolicyCard _card;

        public void Initialize()
        {
            cardImage.sprite = cardInfo.cardImage;
            _card = GetComponentInParent<IPolicyCard>();
        }
        
        public void Select()
        {
            _card.SetPolicyCard(cardInfo);
        }
    }
}