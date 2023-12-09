using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace UI
{
    public class GarageTimerController : MonoBehaviour
    {
        [SerializeField] public float escapeTime = 90;
        public TextMeshProUGUI countdownUI;
        public TextMeshProUGUI objective;
        private float countdown;
        private float min;
        private float sec;
        private bool isBoxDown = false;
        private bool playerEscaped = false;

        void Start()
        {
            PlayerEventManager.OnBoxFall += OnBoxFallEvent;
            PlayerEventManager.OnPlayerTimerEscape += OnPlayerEscape;
        }

        void Update()
        {
            if (playerEscaped)
            {
                countdownUI.text = "";
                objective.text = "Enter the sewers!";
            }
            
            if (isBoxDown && playerEscaped == false)
            {
                min = Mathf.FloorToInt((countdown - Time.deltaTime) / 60);
                sec = Mathf.FloorToInt((countdown - Time.deltaTime) % 60);
                countdownUI.text = $"{min:00}:{sec:00}";
                objective.text = "Use the box to escape before the timer runs out!";
                countdown -= Time.deltaTime;
            }
            
            if (playerEscaped == false && isBoxDown && countdown <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        private void OnBoxFallEvent()
        {
            Debug.Log("Box fell down!");
            countdown = escapeTime;
            isBoxDown = true;
            PlayerEventManager.OnBoxFall -= OnBoxFallEvent;
        }

        private void OnPlayerEscape()
        {
            PlayerEventManager.OnPlayerTimerEscape -= OnPlayerEscape;

            playerEscaped = true;
        }
    }
}