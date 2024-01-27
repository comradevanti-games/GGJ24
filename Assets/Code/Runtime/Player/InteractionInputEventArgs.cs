using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class InteractionInputEventArgs {

		public Vector3 PlayerPosition { get; set; }

		public InteractionInputEventArgs(Vector3 playerPos) {
			PlayerPosition = playerPos;
		}

	}

}