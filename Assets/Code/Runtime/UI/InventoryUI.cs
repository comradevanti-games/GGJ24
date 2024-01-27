#nullable enable

using UnityEngine;
using UnityEngine.UI;

namespace Dev.ComradeVanti.GGJ24
{
    public class InventoryUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject propUIPrefab = null!;
        [SerializeField] private Transform parentTransform = null!;

        #endregion


        #region Methods

        private void Clear()
        {
            for (var i = 0; i < parentTransform.childCount; i++)
            {
                var child = parentTransform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        private void AddPropDisplay(IProp prop)
        {
            var display = Instantiate(propUIPrefab, parentTransform);
            display.GetComponent<Image>().sprite = prop.Thumbnail;
        }

        private void Display(Inventory inventory)
        {
            Clear();
            foreach (var prop in inventory.Props)
                AddPropDisplay(prop);
        }


        private void Awake()
        {
            Singletons.Require<IInventoryKeeper>()
                      .LiveInventoryChanged += args => Display(args.Inventory);
        }

        #endregion
    }
}