using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _1.Code.Systems.PolicyCard
{
    public class PolicyCardItem : MonoBehaviour
    {
        
        [field : SerializeField] public PolicyCardInfo cardInfo { get; private set; }
        [SerializeField] private Image cardImage;
       
    }
}