using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawns
{
    public class SpawnPointMark : MonoBehaviour
    {
        [SerializeField] private SpawnID idSpawn;

        public SpawnID IdSpawn { get => idSpawn; }

        public static SpawnPointMark GetSpawnPointById(SpawnID id)
        {

            SpawnPointMark[] spawns = FindObjectsOfType<SpawnPointMark>();

            for (int i = 0; i < spawns.Length; i++)
            {
                if (id == spawns[i].IdSpawn)
                    return spawns[i];
            }

            return null;
            
        }
    }

    public enum SpawnID
    { 
        numero1,
        numero2,
        numero3,
        numero4,
        numero5,
        numero6,
        numero7,
        numero8,
        numero9,
        numero10,
        numero11,
        numero12,

    }
}
