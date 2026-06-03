using Behaviors.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base_Scripts
{
    public class CheatManager : MonoSingleton<CheatManager>
    {
        [SerializeField] private GameObject chickenBombPrefab;
        
        private float _invincibleTime = 12f;

        void Update()
        {
            CheckCheatShortcuts();
        }
        private void CheckCheatShortcuts()
        {
            // checks if right/left alt is pressed
            bool isAltDown = Input.GetKey(KeyCode.LeftAlt) ||
                              Input.GetKey(KeyCode.RightAlt);
            if (isAltDown)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) 
                {
                    RestartGame();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    RestartLevel();
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ResetPlayerBombs();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ResetPlayerExplosionRadius();
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.ResetLives();
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    var playerHit = FindAnyObjectByType<PlayerHit>();
                    if (playerHit != null)
                    {
                        playerHit.ActivateInvincibility(_invincibleTime);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    SpawnChickenBombCheat();
                }
            }
        }
        private void RestartGame()
        {
            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }
            SceneManager.LoadScene("MainMenu");
        }
        private void RestartLevel()
        {
            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private void ResetPlayerBombs()
        {
            var bombManager = FindAnyObjectByType<PlayerBombManager>();
            if (bombManager != null)
            {
                bombManager.ResetMaxBombs();
            }
        }
        private void ResetPlayerExplosionRadius()
        {
            var bombManager = FindAnyObjectByType<PlayerBombManager>();
            if (bombManager != null)
            {
                bombManager.ResetExplosionRadius();
            }
        }
        private void SpawnChickenBombCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            SoundManager.Instance.PlaySFX(SoundManager.Instance.chickenSpawn);
            Instantiate(chickenBombPrefab, player.transform.position, Quaternion.identity);
        }
    }
}
