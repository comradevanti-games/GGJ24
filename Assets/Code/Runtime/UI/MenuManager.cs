using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Dev.ComradeVanti.GGJ24.UI {

	public class MenuManager : MonoBehaviour {

#region Events

		public UnityEvent playButtonPressed;

#endregion

#region Fields

		[SerializeField] private Canvas menuCanvas;
		[SerializeField] private CanvasGroup menuCanvasGroup;
		[SerializeField] private GameObject tutorialInfoBox;

#endregion

#region Properties

		private InputHandler InputHandler { get; set; }

#endregion

#region Methods

		private void Awake() {
			menuCanvas.worldCamera = Camera.main;
			InputHandler = FindAnyObjectByType<InputHandler>();
			InputHandler.PauseInputPerformed += ShowCanvas;
		}

		public void OnPlayButtonPressed() {
			playButtonPressed?.Invoke();
			HideCanvas();
		}

		public void OnTutorialButtonPressed() {
			tutorialInfoBox.SetActive(!tutorialInfoBox.activeSelf);
		}

		private void HideCanvas() {
			menuCanvasGroup.alpha = 0;
		}

		private void ShowCanvas() {
			menuCanvasGroup.alpha = 1;
		}

		public void OnQuitButtonPressed() {
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

#endregion

	}

}