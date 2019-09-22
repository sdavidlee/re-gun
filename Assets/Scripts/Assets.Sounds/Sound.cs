using System;
using UnityEngine;

namespace Assets.Sounds
{
    [Serializable]
    public class Sound : MonoBehaviour
    {
        [SerializeField] private float _pitch;
        public float Pitch { get => Mathf.Clamp(_pitch, min: 0, max: 3); }
        [SerializeField] private float _volume;
        public float Volume { get => _volume; }
        [SerializeField] private AudioClip _audio;
        public AudioClip AudioClip { get => _audio; }
    }
}