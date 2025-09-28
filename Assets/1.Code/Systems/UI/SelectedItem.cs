using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _1.Code.Systems.UI
{
    public class SelectedItem : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerMoveHandler,
        IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private Canvas rootCanvas;
        
        private Vector2 _prevPos;

        private bool _isSelect;
        private Transform _originParent;
        private int _originIndex;

        private void Start()
        {
            _originParent = transform.parent;
            _originIndex = transform.GetSiblingIndex();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("<color=yellow>선택 가능</color>");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isSelect = true;
            Debug.Log("<color=blue>선택</color>");
            transform.SetParent(rootCanvas.transform, worldPositionStays: true);
            _prevPos = transform.position;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            bool canMove = _isSelect;
            if (canMove)
            {
                transform.position = eventData.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("<color=green>선택 해제</color>");
            transform.SetParent(_originParent, worldPositionStays: true);
            transform.SetSiblingIndex(_originIndex);
            transform.position = _prevPos;
            _isSelect = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelect)
            {
                _isSelect = false;
                Debug.Log("<color=red>선택 범위 이탈</color>");
            }
        }
    }
}