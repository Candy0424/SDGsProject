using System;
using System.Linq;
using System.Collections.Generic;
using _1.Code.Systems.Turn;
using UnityEngine;

namespace _1.Code.Managerrs
{
    public class TurnManager : MonoBehaviour
    {
        public Action OnTurnChangeEvent;

        private List<ITurnAble> _turnLst;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _turnLst = GetComponentsInChildren<ITurnAble>().ToList();
            
            _turnLst.ForEach(turn => OnTurnChangeEvent += turn.TurnInit);
        }

        private void OnDestroy()
        {
            _turnLst.ForEach(turn => OnTurnChangeEvent -= turn.TurnInit);
        }

        public void HandleTurnChange()
        {
            OnTurnChangeEvent?.Invoke();
        }
    }
}