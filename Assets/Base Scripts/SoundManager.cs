using UnityEngine;
using System.Collections;

namespace Base_Scripts
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        
        public AudioClip menuMusic;
        public AudioClip levelMusic;
        public AudioClip gameOverMusic;
        public AudioClip gameWonMusic;
        
        public AudioClip bombDrop;
        public AudioClip bombExplode;
        public AudioClip playerHit;
        public AudioClip pauseSound;
        public AudioClip stepLeftRightSound; 
        public AudioClip stepUpDownSound; 
        public AudioClip powerupPickup;
        
        public AudioClip chickenSpawn; 
        public AudioClip chickenHit;

        private float _musicResumeTime;

        void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
        public void PlayMusic(AudioClip clip)
        {
            if (musicSource.clip == clip && musicSource.isPlaying) return;
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
        public void StopMusic()
        {
            musicSource.Stop();
        }
        public void PlayTemporaryMusic(AudioClip clip)
        {
            StartCoroutine(TemporaryMusicRoutine(clip));
        }
        private IEnumerator TemporaryMusicRoutine(AudioClip clip)
        {
            float savedTime = musicSource.time;
            AudioClip savedClip = levelMusic;
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
            yield return new WaitForSeconds(clip.length);
            musicSource.clip = savedClip;
            musicSource.time = savedTime;
            musicSource.Play();
        }
        public void MusicPause(bool isPaused)
        {
            if (isPaused)
            {
                musicSource.Pause();
            }
            else
            {
                musicSource.UnPause();
            }
        }
    }
}
