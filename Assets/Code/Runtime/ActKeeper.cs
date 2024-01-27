#nullable enable

using System;
using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class ActKeeper : MonoBehaviour, IActKeeper
    {
        public event Action<IActKeeper.ActChangedArgs>? ActChanged;


        private void StartAct(IAct act)
        {
            ActChanged?.Invoke(new IActKeeper.ActChangedArgs(act));
        }

        private async void LoadAndStartAct(int actIndex)
        {
            var act = await ActIO.TryLoadAsync(actIndex);
            if (act == null)
            {
                // TODO: Handle act not existing. Transition to next scene?
                throw new Exception($"Act {actIndex} not found!");
            }

            StartAct(act);
        }

        private void LoadAndStartCurrentAct()
        {
            var actIndex = ActIndexPersistence.Get();
            LoadAndStartAct(actIndex);
        }

        private void Start()
        {
            LoadAndStartCurrentAct();
        }
    }
}