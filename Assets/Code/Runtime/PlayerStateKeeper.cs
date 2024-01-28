using UnityEngine;

namespace Dev.ComradeVanti.GGJ24
{
    public class PlayerStateKeeper : MonoBehaviour, IPlayerStateKeeper
    {
        public PlayerState PlayerState { get; set; } = new PlayerState();
    }
}