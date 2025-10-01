using System;
using System.Collections.Generic;
using _1.Code.Defines;
using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.UI;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.Events;

namespace _1.Code.Managers
{
    public struct CardInfoData
    {
        public int cardNum;
        public PolicyCardInfo item;
    }

    public class PolicyCardManager : MonoBehaviour, IPolicyCard
    {
        [SerializeField] private GoldAmount goldAmount;
        [SerializeField] private HUDManager hud;

        private Dictionary<HUDTypes, List<PolicyCardInfo>> _policyCards;
        private Dictionary<HUDTypes, List<CardInfoData>> _policyItems;

        [SerializeField] private PolicyCardInfo currentCardInfo;
        [SerializeField] private Transform cardHolder;

        [SerializeField] private int maxCardNum;
        private int _currentCardNum;

        [SerializeField] private SoundID useSound;
        
        private Dictionary<PolicyCardInfo, Stack<PolicyCardItem>> _recordCard;
        
        private void Awake()
        {
            _policyCards = new Dictionary<HUDTypes, List<PolicyCardInfo>>();
            _policyItems = new Dictionary<HUDTypes, List<CardInfoData>>();
            _recordCard = new Dictionary<PolicyCardInfo, Stack<PolicyCardItem>>();

            Initialize();
        }

        private void Initialize()
        {
            for (int i = 1; i <= (int)HUDTypes.Gold; i++)
            {
                _policyCards.Add((HUDTypes)i, new List<PolicyCardInfo>());
                _policyItems.Add((HUDTypes)i, new List<CardInfoData>());
            }
        }

        public void HandleBuyCard(PolicyCardItem item)
        {
            AddCard(item.CardInfo);

            PolicyCardItem newItem;
            if (_recordCard.ContainsKey(item.CardInfo) && _recordCard[item.CardInfo].Count > 0)
            {
                newItem = _recordCard[item.CardInfo].Pop();
            }
            else
            {
                newItem = Instantiate(item, cardHolder);
            }

            newItem.Initialize(item.CardInfo, 1, true);
            newItem.gameObject.SetActive(true);
            newItem.OnUseEvent += HandleUseCard;
        }

        private void HandleUseCard(PolicyCardItem item)
        {
            UsePolicyCard();
            RecordCard(item);
        }

        public bool IsInRange() => _currentCardNum < maxCardNum;

        private void RecordCard(PolicyCardItem item)
        {
            item.gameObject.SetActive(false);
            if (!_recordCard.ContainsKey(item.CardInfo))
            {
                _recordCard.Add(item.CardInfo, new Stack<PolicyCardItem>());
            }

            _recordCard[item.CardInfo].Push(item);
        }

        private void AddCard(PolicyCardInfo info)
        {
            HUDTypes type = (HUDTypes)info.cardType;
            List<PolicyCardInfo> l = _policyCards[type];

            if (l.Contains(info))
            {
                int idx = _policyCards[type].IndexOf(info);
                CardInfoData infoData = _policyItems[type][idx];
                infoData.cardNum++;
                _policyItems[type][idx] = infoData;
            }
            else
            {
                _policyCards[type].Add(info);
                CardInfoData infoData = new CardInfoData();
                infoData.cardNum = 1;
                infoData.item = info;
                _policyItems[type].Add(infoData);
            }
            _currentCardNum++;
        }

        private void RemoveCard(PolicyCardInfo info)
        {
            HUDTypes type = (HUDTypes)info.cardType;
            List<PolicyCardInfo> l = _policyCards[type];

            if (l.Contains(info))
            {
                int idx = _policyCards[type].IndexOf(info);
                CardInfoData infoData = _policyItems[type][idx];
                infoData.cardNum--;
                if (infoData.cardNum <= 0)
                {
                    _policyCards[type].Remove(info);
                    _policyItems[type].RemoveAt(idx);
                }
                else
                    _policyItems[type][idx] = infoData;
            }
        }

        public List<PolicyCardInfo> GetPolicyCard(HUDTypes type) => _policyCards.GetValueOrDefault(type);

        private void UsePolicyCard()
        {
            _currentCardNum--;
            useSound.Play();
            foreach (var data in currentCardInfo.datas)
            {
                if (data.valueType == HUDTypes.Gold)
                {
                    goldAmount.AmountCalc(data.value);
                    continue;
                }

                HUD type = hud.GetHUD(data.valueType);
                type.GageCalc(data.value);
            }

            RemoveCard(currentCardInfo);
        }

        public void SetPolicyCard(PolicyCardInfo info) => currentCardInfo = info;
    }
}