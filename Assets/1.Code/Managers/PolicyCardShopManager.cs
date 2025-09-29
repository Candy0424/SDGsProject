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

        private Dictionary<PolicyCardInfo, PolicyCardItem> _recordCard;

        private void Awake()
        {
            items = new List<PolicyCardItem>();
            _recordCard = new Dictionary<PolicyCardInfo, PolicyCardItem>();
        }

        public void TurnInit()
        {
            ShopInit();
        }
        
        private void ShopInit()
        {
            if (items.Count > 0)
            {
                items.Clear();
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
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

                if (_recordCard.ContainsKey(cardArr[idx]))
                {
                    _recordCard[cardArr[idx]].gameObject.SetActive(true);
                    items.Add(_recordCard[cardArr[idx]].Initialize(cardArr[idx], size));
                }
                else
                {
                    PolicyCardItem item = Instantiate(itemArr[itemIdx], transform).Initialize(cardArr[idx], size);
                    _recordCard.Add(cardArr[idx], item);
                }
            }
        }

        public void TurnEnd()
        {
        }
    }
}