using System.Collections.Generic;
using _1.Code.Defines;
using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.UI;
using UnityEngine;

namespace _1.Code.Managers
{
    public class PolicyCardManager : MonoBehaviour, IPolicyCard
    {
        [SerializeField] private HUDManager hud;
        
        private Dictionary<HUDTypes, List<PolicyCardItem>> _policyCards;

        [SerializeField] private PolicyCardInfo currentCardInfo;
        
        private void Awake()
        {
            _policyCards = new Dictionary<HUDTypes, List<PolicyCardItem>>();
            
            Initialize();
        }

        private void Initialize()
        {
            IEnumerable<PolicyCardItem> cardLst = GetComponentsInChildren<PolicyCardItem>();

            foreach (PolicyCardItem item in cardLst)
            {
                _policyCards[item.cardInfo.cardType].Add(item);
            }
        }

        public List<PolicyCardItem> GetPolicyCard(HUDTypes type) => _policyCards.GetValueOrDefault(type);

        public void UsePolicyCard()
        {
            foreach (var data in currentCardInfo.datas)
            { 
                HUD type = hud.GetHUD(data.valueType);
                type.HUDDeltaCalc(data.value);
            }
        }

        public void SetPolicyCard(PolicyCardInfo info) => currentCardInfo = info;
    }
}