#nullable enable

using System;
using System.Linq;
using Dev.ComradeVanti.GGJ24.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.ComradeVanti.GGJ24
{
    public class LiveInventoryPanelManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject propUIPrefab = null!;
        [SerializeField] private Transform parentTransform = null!;
        [SerializeField] private Color highlightedColor = Color.white;
        [SerializeField] private Color regularColor = Color.white;

        private readonly GameObject[] items =
            new GameObject[Inventory.MaxItemCount];

        #endregion

        #region Methods

        private void Display(Inventory inventory, int? selectedIndex)
        {
            for (var index = 0; index < Inventory.MaxItemCount; index++)
            {
                var prop = inventory.Props.ElementAtOrDefault(index);
                var item = items[index];

                item.SetActive(prop != null);
                if (prop == null) continue;

                var isSelected = selectedIndex == index;
                var image = item.GetComponent<Image>();

                image.color = isSelected ? highlightedColor : regularColor;
                image.sprite = prop.Thumbnail;
            }
        }

        private void SpawnItems()
        {
            for (var i = 0; i < Inventory.MaxItemCount; i++)
                items[i] = Instantiate(propUIPrefab, parentTransform);
        }

        private void UpdateVisibilityForPhase(PlayerPhase phase)
        {
            var visible = phase is PlayerPhase.PropSelection or PlayerPhase.Setup;
            gameObject.SetActive(visible);
        }

        private void Awake()
        {
            SpawnItems();
            Singletons.Require<IInventoryKeeper>().LiveInventoryChanged +=
                args => Display(args.Inventory, args.SelectedPropIndex);
            Singletons.Require<IPhaseKeeper>().PhaseChanged +=
                args => UpdateVisibilityForPhase(args.NewPhase);
        }

        #endregion
    }
}