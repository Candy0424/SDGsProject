using System.Collections.Generic;
using _1.Code.Defines;
using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.UI;
using UnityEngine;

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

        private void Awake()
        {
            _policyCards = new Dictionary<HUDTypes, List<PolicyCardInfo>>();
            _policyItems = new Dictionary<HUDTypes, List<CardInfoData>>();

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

        void AddCard(PolicyCardInfo info)
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

        public void UsePolicyCard()
        {
            foreach (var data in currentCardInfo.datas)
            {
                if (data.valueType == HUDTypes.Gold)
                {
                    goldAmount.AmountCalc(data.value);
                    continue;
                }

                HUD type = hud.GetHUD(data.valueType);
                type.HUDDeltaCalc(data.value);
            }

            RemoveCard(currentCardInfo);
        }

        public void SetPolicyCard(PolicyCardInfo info) => currentCardInfo = info;
    }
}