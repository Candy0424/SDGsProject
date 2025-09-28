using System;
using UnityEngine;

namespace _1.Code.Systems.UI
{
    public class SelectedManager : MonoBehaviour
    {
        private bool _isSelect;

        private void Start()
        {
            _isSelect = false;
        }

        public void Select()
        {
            _isSelect = true;
        }

        public void Deselect()
        {
            _isSelect = false;
        }
        
        public bool IsSelect() => _isSelect;
    }
}