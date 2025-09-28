using _1.Code.Systems.PolicyCard;
using _1.Code.Systems.Turn;
using UnityEngine;

namespace _1.Code.Managers
{
    public class PolicyCardShopManager : MonoBehaviour, ITurnAble
    {
        [SerializeField] private PolicyCardInfo[] cardArr;
        [SerializeField] private int cardNum;

        [SerializeField] private PolicyCardItem[] cardPrefabs;
        public void TurnInit()
        {
            ShopInit();
        }
        
        private void ShopInit()
        {
            for (int i = 0; i < cardNum; i++)
            {
                int idx = Random.Range(0, cardArr.Length);
                PolicyCardItem item = Instantiate(cardPrefabs[idx], transform);
            }
        }

        public void TurnEnd()
        {
            throw new System.NotImplementedException();
        }
    }
}