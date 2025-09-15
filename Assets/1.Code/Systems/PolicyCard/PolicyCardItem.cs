using UnityEngine;

namespace _1.Code.Systems.PolicyCard
{
    public class PolicyCardItem : MonoBehaviour
    {
        [field : SerializeField] public PolicyCardInfo cardInfo { get; private set; }
    }
}