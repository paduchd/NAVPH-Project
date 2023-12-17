using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


namespace UI
{
    // Manages the UI parts where timer in garage is shown
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

        // Subcribing to events
        void Start()
        {
            PlayerEventManager.OnBoxFall += OnBoxFallEvent;
            PlayerEventManager.OnPlayerTimerEscape += OnPlayerEscape;
        }

        // Refreshing the timer
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

        // We dont need the event listener after first occurence
        private void OnBoxFallEvent()
        {
            countdown = escapeTime;
            isBoxDown = true;
            PlayerEventManager.OnBoxFall -= OnBoxFallEvent;
        }

        // End the timer after the player escaped
        private void OnPlayerEscape()
        {
            PlayerEventManager.OnPlayerTimerEscape -= OnPlayerEscape;

            playerEscaped = true;
        }
    }
}