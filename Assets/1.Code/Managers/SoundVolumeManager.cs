using Ami.BroAudio;
using UnityEngine;

namespace _1.Code.Managers
{
    public class SoundVolumeManager : MonoBehaviour
    {
        [SerializeField] private BroAudioType master;
        [SerializeField] private BroAudioType sfx;
        [SerializeField] private BroAudioType bgm;

        public void ChangeMaster(float value)
        {
            BroAudio.SetVolume(master, value);
        }

        public void ChangeSfx(float value)
        {
            BroAudio.SetVolume(sfx, value);
        }

        public void ChangeBgm(float value)
        {
            BroAudio.SetVolume(bgm, value);
        }
    }
}