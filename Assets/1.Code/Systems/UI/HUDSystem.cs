using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class HUDSystem : MonoBehaviour
    {
        private List<HUD> _hudLst;

        private void Awake()
        {
            _hudLst = new List<HUD>();

            HUDInit();
        }

        private void HUDInit()
        {
            _hudLst = GetComponentsInChildren<HUD>().ToList();
            
            _hudLst.ForEach(hud => hud.Initialize());
        }
    }
}