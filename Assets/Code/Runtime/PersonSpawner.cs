#nullable enable

using UnityEngine;

namespace Dev.ComradeVanti.GGJ24 {

	public class PersonSpawner : MonoBehaviour, IPersonSpawner {

		[SerializeField] private GameObject personPrefab = null!;

		public GameObject SpawnPerson(Vector3 position) {
			var person = Instantiate(personPrefab, position, Quaternion.identity);
			LivePerson lp = person.GetComponent<LivePerson>();
			lp.EnableHat();
			lp.SetRandomHairColor();
			return person;
		}

	}

}