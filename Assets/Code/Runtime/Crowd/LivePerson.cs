using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class LivePerson : MonoBehaviour {

		[SerializeField] private GameObject personHat;
		[SerializeField] private GameObject hair;
		[SerializeField] private Animation animationKeeper;

		public void EnableHat() {

			float choice = Random.Range(0f, 1f);
			personHat.SetActive(choice > 0.5f ? true : false);

		}

		public void SetRandomHairColor() {

			hair.GetComponent<SkinnedMeshRenderer>().material.color =
				new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

			if (personHat.activeSelf) hair.SetActive(false);

		}

		public void SetHahaScore(float score) { }

	}

}