using UnityEngine;

namespace Assets.Sounds
{
    public class SoundEffects : MonoBehaviour
    {
        [SerializeField] private Sound[] sounds;
        private AudioSource AudioSource { get; set; }

        private void Awake()
        {
            this.AudioSource = GetComponent<AudioSource>();
        }

        private void PlaySoundEffect(int index)
        {
            var theSound = this.sounds[index];
            AudioSource.clip = theSound.AudioClip;
            AudioSource.volume = theSound.Volume;
            AudioSource.pitch = theSound.Pitch;
            AudioSource.Play();
        }
    }
}