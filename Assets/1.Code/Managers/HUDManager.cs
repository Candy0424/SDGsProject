using System;
using System.Collections.Generic;
using System.Linq;
using _1.Code.Defines;
using _1.Code.Systems.UI;
using UnityEngine;

namespace _1.Code.Managers
{
    public class HUDManager : MonoBehaviour
    {
        private Dictionary<HUDTypes, HUD> _hudTypeDict;

        private void Awake()
        {
            _hudTypeDict = new Dictionary<HUDTypes, HUD>();

            Initialize();
        }

        private void Initialize()
        {
            GetComponentsInChildren<HUD>().ToList().ForEach(hud => _hudTypeDict.Add(hud.HudAttribute, hud));
            foreach (var hud in _hudTypeDict.Values)
                hud.Initialize();
        }

        public HUD GetHUD(HUDTypes targetType)
        {
            if (!_hudTypeDict.TryGetValue(targetType, out HUD hud))
            {
                Debug.LogError($"There is not Key : {targetType}");
                return null;
            }

            return hud;
        }
    }
}