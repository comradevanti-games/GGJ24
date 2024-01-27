using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    /// <summary>
    /// An asset that contains data for a pre-made prop
    /// </summary>
    public class PropAsset : ScriptableObject, IProp
    {
        [SerializeField] private GameObject prefab;


        public GameObject Prefab => prefab;
    }
}