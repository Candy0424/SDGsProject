using System;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Code.Systems.UI
{
    public class PlanetStatus : MonoBehaviour
    {
        [SerializeField] private Sprite badStatus;
        [SerializeField] private Sprite goodStatus;
        
        private Image _status;

        private void Awake()
        {
            _status = GetComponent<Image>();
        }

        public void BadStatus()
        {
            _status.sprite = badStatus;
        }
        
        public void GoodStatus()
        {
            _status.sprite = goodStatus;
        }
    }
}