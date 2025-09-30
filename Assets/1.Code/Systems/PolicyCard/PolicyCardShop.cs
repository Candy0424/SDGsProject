using System;
using System.Collections.Generic;
using _1.Code.Managers;
using _1.Code.Systems.Turn;
using _1.Code.Systems.UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _1.Code.Systems.PolicyCard
{
    public class PolicyCardShop : MonoBehaviour, ITurnAble
    {
        [SerializeField] private PolicyCardManager policyCardManager;
        
        [SerializeField] private GoldItem goldItem;
        [SerializeField] private PolicyCardInfo[] cardArr;
        [SerializeField] private int cardNum;

        [SerializeField] private PolicyCardItem[] itemArr;
        [SerializeField] private float size;
        
        [SerializeField] private List<PolicyCardItem> items;
        
        private Dictionary<PolicyCardInfo, Stack<PolicyCardItem>> _recordCard;

        private void Awake()
        {
            items = new List<PolicyCardItem>();
            _recordCard = new Dictionary<PolicyCardInfo, Stack<PolicyCardItem>>();
        }
        
        public UnityEvent<PolicyCardItem> OnBuyEvent;

        public void TurnInit()
        {
            ShopInit();
        }
        
        private void ShopInit()
        {
            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    RecordCard(item);
                }
                items.Clear();
            }
            
            for (int i = 0; i < cardNum; i++)
            {
                int idx = Random.Range(0, cardArr.Length);
                int itemIdx = (int)cardArr[idx].cardType - 1;

                if (itemIdx < 0 || itemIdx >= cardArr.Length)
                {
                    Debug.LogError($"Item Idx is Out of Range : {itemIdx}");
                    return;
                }

                PolicyCardItem item;
                if (_recordCard.ContainsKey(cardArr[idx]) && _recordCard[cardArr[idx]].Count > 0)
                {
                    item = _recordCard[cardArr[idx]].Pop();
                }
                else
                {
                    item = Instantiate(itemArr[itemIdx], transform);
                } 
                item.Initialize(cardArr[idx], size);
                item.OnBuyEvent += BuyItem;
                item.gameObject.SetActive(true);
                items.Add(item);
            }
        }

        private void BuyItem(PolicyCardItem item)
        {
            if (policyCardManager.IsInRange() && goldItem.CanBuyGoldCheck(item.CardInfo))
            {
                int price = item.CardInfo.gold;
                goldItem.GoldCalc(-price);
                RecordCard(item);
                items.Remove(item);
                item.OnBuyEvent -= BuyItem;
                OnBuyEvent?.Invoke(item);
            }
        }
        
        private void RecordCard(PolicyCardItem item)
        {
            item.gameObject.SetActive(false);
            if (!_recordCard.ContainsKey(item.CardInfo))
            {
                _recordCard.Add(item.CardInfo, new Stack<PolicyCardItem>());
            }
            _recordCard[item.CardInfo].Push(item);
        }

        public void TurnEnd()
        {
        }
    }
}