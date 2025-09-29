using System;
using System.Collections.Generic;
using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.Turn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Code.Managers
{
    public class PolicyCardShopManager : MonoBehaviour, ITurnAble
    {
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
                    item.gameObject.SetActive(false);
                    if (!_recordCard.ContainsKey(item.CardInfo))
                    {
                        _recordCard.Add(item.CardInfo, new Stack<PolicyCardItem>());
                    }
                    _recordCard[item.CardInfo].Push(item);
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

                if (_recordCard.ContainsKey(cardArr[idx]) && _recordCard[cardArr[idx]].Count > 0)
                {
                    PolicyCardItem item = _recordCard[cardArr[idx]].Pop();
                    item.Initialize(cardArr[idx], size);
                    item.gameObject.SetActive(true);
                    items.Add(item);
                }
                else
                {
                    PolicyCardItem item = Instantiate(itemArr[itemIdx], transform);
                    item.Initialize(cardArr[idx], size);
                    item.gameObject.SetActive(true);
                    items.Add(item);
                }
            }
        }

        public void TurnEnd()
        {
        }
    }
}