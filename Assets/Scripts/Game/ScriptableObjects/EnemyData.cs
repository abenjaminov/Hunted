using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public float Health;
    }
}