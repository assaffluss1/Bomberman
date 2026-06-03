using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base_Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private UIManager _uiManager;
        
        private readonly float _startTime = 200f;
        private float _levelTime = 200f;
        private int _score;
        private int _lives = 2;
        private int _initialLives = 2;
        private bool _isGameOver;
        private bool _isPaused;

        void Awake() 
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        void Start()
        {
            _levelTime = _startTime;
            UpdateUI();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayMusic(SoundManager.Instance.levelMusic);
            }
        }
        void Update()
        {
            if (_isGameOver) return;
            UpdateLevelTime();
            UpdateIsPaused();
        }
        private void UpdateLevelTime()
        {
            if (_levelTime > 0)
            {
                _levelTime -= Time.deltaTime; 
                if (_uiManager != null) 
                {
                    _uiManager.UpdateTime((int)_levelTime);
                }
            }
            else
            {
                LoseLife();
            }
        }
        private void UpdateIsPaused()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _isPaused = !_isPaused;
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pauseSound);
                SoundManager.Instance.MusicPause(_isPaused);
                Time.timeScale = _isPaused ? 0f : 1f;
            }
        }
        public void AddScore(int amount)
        {
            _score += amount;
            if (_uiManager != null)
            {
                _uiManager.UpdateScore(_score);
            }
            SaveHighScore();
        }
        public void SetUIManager(UIManager ui)
        {
            _uiManager = ui;
        }
        public void UpdateUI()
        {
            if (_uiManager != null)
            {
                _uiManager.UpdateScore(_score);
                _uiManager.UpdateLives(_lives);
                _uiManager.UpdateTime((int)_levelTime);
            }
        }
        public void LoseLife()
        {
            _lives--;
            if (_lives < 0)
            {
                EndGame();
            }
            else
            {
                if (_uiManager != null)
                {
                    _uiManager.UpdateLives(_lives);
                }
                _levelTime = _startTime;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        public void WinGame()
        {
            _isGameOver = true;
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayMusic(SoundManager.Instance.gameWonMusic);
            }
            SaveHighScore();
            if (_uiManager != null)
            {
                _uiManager.ShowGameWon();
            }
        }
        private void EndGame()
        {
            _isGameOver = true;
            SaveHighScore();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayMusic(SoundManager.Instance.gameOverMusic);   
            }
            if (_uiManager != null)
            {
                _uiManager.ShowGameOver();
            }
        }
        private void SaveHighScore()
        {
            int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
            if (_score > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", _score);
                PlayerPrefs.Save();
            }
        }
        public void ResetLives()
        {
            _lives = _initialLives; 
            if (_uiManager != null)
            {
                _uiManager.UpdateLives(_lives);
            }
        }
    }
}
