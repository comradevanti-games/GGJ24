#nullable enable

using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// An asset that contains data for a pre-made prop
    /// </summary>
    [CreateAssetMenu(menuName = "GGJ24/Prop", fileName = "New prop")]
    public class PropAsset : ScriptableObject, IProp
    {
        [SerializeField] private GameObject prefab = null!;
        [SerializeField] private Sprite? thumbnail;
        [SerializeField] [TextArea(1, 20)] private string description = "";


        public GameObject Prefab => prefab;

        public Sprite? Thumbnail => thumbnail;

        public string Description => description;
    }
}