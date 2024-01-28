#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dev.ComradeVanti.GGJ24
{
    public class PlaySoundInteractable : MonoBehaviour, IPropInteractable
    {
        [SerializeField] private AudioClip[] clips = Array.Empty<AudioClip>();

        private AudioSource sfxPlayerSource = null!;

        public PropInteraction TryInteraction(int currentSlotIndex, PerformanceState performanceState)
        {
            if (clips.Length > 0)
            {
                var clip = clips[Random.Range(0, clips.Length)];
                sfxPlayerSource.PlayOneShot(clip);
            }

            return new PropInteraction(
                performanceState, ImmutableHashSet<HumorEffect>.Empty);
        }


        private void Awake()
        {
            sfxPlayerSource = GameObject.FindWithTag("SfxPlayer")
                                        .GetComponent<AudioSource>();
        }
    }
}