using System.Collections.Generic;
using _1.Code.Defines;
using _1.Code.Systems.PolicyCard;
using UnityEngine;

namespace _1.Code.Managers
{
    public class PolicyCardManager : MonoBehaviour
    {
        private Dictionary<HUDTypes, List<PolicyCardItem>> _policyCards;

        [SerializeField] private PolicyCardItem currentCard;
        
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
    }
}