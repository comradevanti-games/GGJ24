using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dev.ComradeVanti.GGJ24.UI {

	public class MainMenuManager : MonoBehaviour {

		public void OnPlayButtonPressed() {
			SceneManager.LoadScene("Main");
		}

		public void OnQuitButtonPressed() {
			Application.Quit();
		}

	}

}