using Ami.BroAudio;
using UnityEngine;

namespace _1.Code.Managers
{
    public class LobbyMenuButtonPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private SoundID defaultClickSound;
        [SerializeField] private SoundID startSound;

        public void OnPlaySound()
        {
            PlaySound(defaultClickSound);
        }
        
        public void OnStart()
        {
            PlaySound(startSound);
        }

        public void OnTutorial()
        {
            PlaySound(defaultClickSound);
            tutorialPanel.SetActive(true);
        }
        
        public void OnOption()
        {
            PlaySound(defaultClickSound);
            optionPanel.SetActive(true);
        }
        
        public void OnExit()
        {
            PlaySound(startSound);
            Application.Quit();
        }
        
        private void PlaySound(SoundID soundID) => soundID.Play();
    }
}