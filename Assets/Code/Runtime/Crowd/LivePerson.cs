using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class LivePerson : MonoBehaviour {

		[SerializeField] private GameObject personHat;
		[SerializeField] private GameObject hair;
		[SerializeField] private Animator animationController;
		private static readonly int Idle = Animator.StringToHash("Idle");
		private static readonly int Clap = Animator.StringToHash("Clap");
		private static readonly int Laugh = Animator.StringToHash("Laugh");

		public void EnableHat() {

			float choice = Random.Range(0f, 1f);
			personHat.SetActive(choice > 0.5f);

		}

		public void SetRandomHairColor() {

			hair.GetComponent<SkinnedMeshRenderer>().material.color =
				new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

			if (personHat.activeSelf) hair.SetActive(false);

		}

		public void SetHahaScore(float score) {

			if (score < 1) {
				animationController.SetTrigger(Idle);
			}

			if (score is >= 1 and < 2) {
				animationController.SetTrigger(Clap);
			}

			if (score >= 2) {
				animationController.SetTrigger(Laugh);
			}

		}

	}

}