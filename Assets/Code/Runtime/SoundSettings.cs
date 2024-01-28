using System;
using System.Collections;
using System.Collections.Generic;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using UnityEngine.Audio;

namespace Dev.ComradeVanti.GGJ24 {

	public class SoundSettings : MonoBehaviour {

		public AudioMixer mixer;
		public AudioSource[] MusicResource;
		public AudioClip[] OneShots;

		public void SetLevel(float sliderValue) {
			mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
		}

		public void Awake() {
			Singletons.Require<PhaseKeeper>().PhaseChanged += args => {
				if (args.NewPhase == PlayerPhase.Menu) {
					MusicResource[0].Play();
					MusicResource[1].Pause();
				}
				else {
					if (!MusicResource[1].isPlaying) {
						MusicResource[1].Play();
					}

					MusicResource[0].Pause();
				}

			};

		}

	}

}