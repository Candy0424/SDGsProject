using UnityEngine;
using UnityEngine.Events;

namespace _1.Code.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int deadCount;
        [SerializeField] private int score;
        [SerializeField] private int checkDeadCount;
        [SerializeField] private int limitDay;

        private bool _isGameOver = false;
        
        public UnityEvent<int> OnGameOverEvent;
        public UnityEvent OnGameClearEvent;
        
        public void HandleDeadCount()
        {
            if (_isGameOver) return;
            deadCount++;

            if (checkDeadCount <= deadCount)
            {
                _isGameOver = true;
                OnGameOverEvent.Invoke(score);
            }
        }

        public void GameClearCheck(int currentDay)
        {
            score = currentDay;
            if (currentDay > limitDay)
                OnGameClearEvent?.Invoke();
        }
    }
}