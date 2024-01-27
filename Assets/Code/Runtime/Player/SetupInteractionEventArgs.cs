using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class SetupInteractionEventArgs {

		public Vector3 PlayerPosition { get; set; }

		public SetupInteractionEventArgs(Vector3 playerPos) {
			PlayerPosition = playerPos;
		}

	}

}