using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _1.Code.Systems.UI
{
    public class SelectedItem : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerMoveHandler,
        IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private LayerMask whatIsBuyZone;
        [SerializeField] private float sizeUpValue;
        [SerializeField] private float duration;
        
        private Canvas _rootCanvas;
        private GraphicRaycaster _raycaster;
        
        private Vector2 _prevPos;
        private ISelectAble _selectItem;

        private bool _isSelect;
        private Transform _originParent;
        private int _originIndex;
        private Vector3 _originSize;

        private Tween _tween;

        public UnityEvent OnInteractEvent;
        
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _rootCanvas = transform.root.GetComponent<Canvas>();
            _raycaster = _rootCanvas.GetComponent<GraphicRaycaster>();
            _selectItem = GetComponent<ISelectAble>();
            _originParent = transform.parent;
            _originIndex = transform.GetSiblingIndex();
        }

        public void OnEnable()
        {
            _originSize = transform.localScale;
        }

        private void OnDisable()
        {
            transform.localScale = _originSize;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("<color=yellow>선택 가능</color>");
            _tween = transform.DOScale(new Vector3(sizeUpValue, sizeUpValue, sizeUpValue)
                , duration).SetEase(Ease.OutElastic);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isSelect = true;
            _selectItem.Select();
            //Debug.Log("<color=blue>선택</color>");
            transform.SetParent(_rootCanvas.transform, worldPositionStays: true);
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
            //Debug.Log("<color=green>선택 해제</color>");
            
            List<RaycastResult> l = new List<RaycastResult>();
            _raycaster.Raycast(eventData, l);

            foreach (RaycastResult r in l)
            {
                int layer = r.gameObject.layer;

                if ((whatIsBuyZone.value & (1 << layer)) != 0)
                {
                    OnInteractEvent?.Invoke();
                }
            }
            
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
            }
            _tween = transform.DOScale(_originSize, duration).SetEase(Ease.OutElastic);
        }
    }
}