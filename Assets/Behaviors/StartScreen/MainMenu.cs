using Base_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Behaviors.StartScreen
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI highScoreText;

        void Start()
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = highScore.ToString();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayMusic(SoundManager.Instance.menuMusic);
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }
        private void StartGame()
        {
            SceneManager.LoadScene("Game"); 
        }
    }
}
