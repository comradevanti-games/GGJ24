using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Dev.ComradeVanti.GGJ24
{
    public class SoundSettings : MonoBehaviour
    {
        public AudioMixer mixer;

        public void SetLevel(float sliderValue)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue)*20 );
        }
    }
}
