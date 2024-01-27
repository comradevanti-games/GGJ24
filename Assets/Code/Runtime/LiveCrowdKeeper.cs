#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class LiveCrowdKeeper : MonoBehaviour, ILiveCrowdKeeper
    {
        private IPersonSpawner personSpawner = null!;
        private readonly IList<GameObject> livePeople = new List<GameObject>();


        private void ClearCrowd()
        {
            foreach (var livePerson in livePeople)
                Destroy(livePerson);

            livePeople.Clear();
        }

        private void AddPerson(IPerson _)
        {
            var livePerson = personSpawner.SpawnPerson(Vector3.zero);
            livePeople.Add(livePerson);
        }

        private void ReplaceCrowd(ICrowd crowd)
        {
            ClearCrowd();
            foreach (var person in crowd.People)
                AddPerson(person);
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