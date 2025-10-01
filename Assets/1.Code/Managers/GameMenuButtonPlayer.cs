using Ami.BroAudio;
using UnityEngine;

namespace _1.Code.Managers
{
    public class GameMenuButtonPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID defaultClickSound;
        [SerializeField] private SoundID warningSound;
        [SerializeField] private SoundID startSound;
        
        public void PlayWarningSound() => warningSound.Play();

        public void PlayClickSound() => defaultClickSound.Play();

        public void PlayExitSound() => startSound.Play();
    }
}