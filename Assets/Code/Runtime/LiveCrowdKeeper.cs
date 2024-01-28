#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class LiveCrowdKeeper : MonoBehaviour, ILiveCrowdKeeper
    {
        private const float SpaceBetweenPeople = 0.75f;

        [SerializeField] private Transform crowdCenterTransform = null!;

        private IPersonSpawner personSpawner = null!;
        private readonly IList<GameObject> livePeople = new List<GameObject>();


        private IEnumerable<Vector3> CalculatePeoplePositions(int personCount)
        {
            var crowdCenterPosition = crowdCenterTransform.position;
            var totalSpace = (personCount - 1) * SpaceBetweenPeople;
            var minX = crowdCenterPosition.x - totalSpace / 2;

            for (var i = 0; i < personCount; i++)
            {
                var x = minX + SpaceBetweenPeople * i;
                yield return new Vector3(x, crowdCenterPosition.y, crowdCenterPosition.z);
            }
        }

        private void ClearCrowd()
        {
            foreach (var livePerson in livePeople)
                Destroy(livePerson);

            livePeople.Clear();
        }

        private void AddPerson(Vector3 position, IPerson _)
        {
            var livePerson = personSpawner.SpawnPerson(position);
            livePeople.Add(livePerson);
        }

        private void ReplaceCrowd(ICrowd crowd)
        {
            ClearCrowd();

            var positions = CalculatePeoplePositions(crowd.People.Length)
                .ToImmutableArray();

            for (var index = 0; index < crowd.People.Length; index++)
            {
                var person = crowd.People[index];
                var position = positions[index];
                AddPerson(position, person);
            }
        }

        private void OnActChanged(IActKeeper.ActChangedArgs args)
        {
            ReplaceCrowd(args.Act.Crowd);
        }

        private void Awake()
        {
            personSpawner = Singletons.Require<IPersonSpawner>();
            Singletons.Require<IActKeeper>().ActChanged += OnActChanged;
        }
    }
}