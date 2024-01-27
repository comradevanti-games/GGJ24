using UnityEngine;
using UnityEngine.UI;

namespace Dev.ComradeVanti.GGJ24 {

	public class PropUI : MonoBehaviour {

		[SerializeField] private Image thumbnail;

		public IProp PropData { get; set; }
		public Image PropSpriteRenderer {
			get => thumbnail;
			set => thumbnail = value;
		}

	}

}