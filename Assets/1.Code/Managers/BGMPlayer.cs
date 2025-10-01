using System;
using Ami.BroAudio;
using UnityEngine;

namespace _1.Code.Managers
{
    public class BGMPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID bgm;

        private void Start()
        {
            bgm.Play();
        }
    }
}