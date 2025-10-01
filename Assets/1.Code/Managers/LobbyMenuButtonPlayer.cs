using Ami.BroAudio;
using UnityEngine;

namespace _1.Code.Managers
{
    public class LobbyMenuButtonPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private SoundID clickSound;
        
        public void Tutorial()
        {
            
        }
        
        public void Option()
        {
            optionPanel.SetActive(true);
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}