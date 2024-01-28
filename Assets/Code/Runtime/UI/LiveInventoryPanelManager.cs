#nullable enable

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.ComradeVanti.GGJ24
{
    public class LiveInventoryPanelManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject propUIPrefab = null!;
        [SerializeField] private Transform parentTransform = null!;

        private readonly GameObject[] items =
            new GameObject[Inventory.MaxItemCount];

        #endregion


        #region Methods

        private void AddPropDisplay(IProp prop, int index)
        {
        }

        private void Display(Inventory inventory)
        {
            for (var index = 0; index < Inventory.MaxItemCount; index++)
            {
                var prop = inventory.Props.ElementAtOrDefault(index);
                var item = items[index];
                
                item.SetActive(prop!= null);
                
                if (prop == null) continue;
                item.GetComponent<Image>().sprite = prop.Thumbnail;
            }
        }

        private void SpawnItems()
        {
            for (var i = 0; i < Inventory.MaxItemCount; i++)
                items[i] = Instantiate(propUIPrefab, parentTransform);
        }

        private void Awake()
        {
            SpawnItems();
            Singletons.Require<IInventoryKeeper>()
                      .LiveInventoryChanged += args => Display(args.Inventory);
        }

        #endregion
    }
}