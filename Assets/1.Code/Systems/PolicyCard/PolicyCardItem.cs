using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _1.Code.Systems.PolicyCard
{
    // public delegate bool OnBoolReturnEvent<T>(T , T);
    public class PolicyCardItem : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField] private InputSystemSO inputSystem;
        [SerializeField] private Transform trm;
        [field : SerializeField] public PolicyCardInfo cardInfo { get; private set; }
        [SerializeField] private Image cardImage;
        
        
        private bool isSelect;
        private Vector2 _prevPos;
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("<color=yellow>선택 가능</color>");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("<color=blue>선택</color>");
            _prevPos = trm.position;
            OnPointerEnterEvent.AddListener(CheckBool);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (isSelect)
            {
                Vector2 pos = inputSystem.GetMousePos();
                trm.position = eventData.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("<color=green>선택 해제</color>");
            trm.position = _prevPos;
            isSelect = false;
        }
    }
}