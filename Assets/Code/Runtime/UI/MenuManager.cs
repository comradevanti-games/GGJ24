using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dev.ComradeVanti.GGJ24.UI {

	public class MenuManager : MonoBehaviour {

#region Events

		public UnityEvent playButtonPressed;

		public UnityEvent tutorialButtonPressed;

#endregion

#region Methods

		public void OnPlayButtonPressed() {
			playButtonPressed?.Invoke();
		}

		public void OnTutorialButtonPressed() {
			tutorialButtonPressed?.Invoke();
		}

		public void OnQuitButtonPressed() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

#endregion

	}

}