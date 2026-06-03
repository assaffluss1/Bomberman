using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Base_Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI livesText;
        
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameWonPanel;
        
        void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetUIManager(this);
                GameManager.Instance.UpdateUI(); 
            }
        }
        void Update()
        {
            if (IsEndScreenActive())
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    BackToMainMenu();
                }
            }
        }
        private bool IsEndScreenActive()
        {
            return (gameOverPanel != null && gameOverPanel.activeSelf) || 
                   (gameWonPanel != null && gameWonPanel.activeSelf);
        }
        public void UpdateTime(int time)
        {
            timeText.text = "TIME " + time.ToString();
        }
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString("00");
        }
        public void UpdateLives(int lives)
        {
            livesText.text = "LEFT " + lives.ToString();
        }
        public void ShowGameOver()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        public void ShowGameWon()
        {
            if (gameWonPanel != null)
            {
                gameWonPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        public void BackToMainMenu()
        {
            Time.timeScale = 1f;
            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
}
