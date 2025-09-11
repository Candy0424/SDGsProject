using System;
using System.Collections.Generic;
using System.ComponentModel;
using _1.Code.Defines;
using UnityEngine;

namespace _1.Code.Systems.PolicyCard
{
    [Serializable]
    public struct HUDDatas
    {
        public HUDTypes valueType;
        public int value;
    }
    
    [CreateAssetMenu(fileName = "PolicyCardInfo", menuName = "SO/PolicyCardInfo", order = 0)]
    public class PolicyCardInfo : ScriptableObject
    {
        public string cardName;
        [TextArea] public string cardDescription;
        public Sprite cardImage;

        public List<HUDDatas> datas;
    }
}