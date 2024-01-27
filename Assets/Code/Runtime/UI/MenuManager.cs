using UnityEngine;
using UnityEngine.Events;

namespace Dev.ComradeVanti.GGJ24.UI {

	public class MenuManager : MonoBehaviour {

#region Events

		public UnityEvent playButtonPressed;

		public UnityEvent tutorialButtonPressed;

#endregion

#region Fields

		[SerializeField] private Canvas menuCanvas;
		[SerializeField] private CanvasGroup menuCanvasGroup;

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
			tutorialButtonPressed?.Invoke();
		}

		private void HideCanvas() {
			menuCanvasGroup.alpha = 0;
		}

		private void ShowCanvas() {
			menuCanvasGroup.alpha = 1;
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