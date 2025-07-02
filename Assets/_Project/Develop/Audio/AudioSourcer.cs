using R3;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourcer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        public void ChangeVolume(float volume)
        {
            _audioSource.volume = volume;
        }

        public void PlayOneShot(AudioClip clip)
        {
            _audioSource.loop = false;
            _audioSource.PlayOneShot(clip);
        }
    }
}