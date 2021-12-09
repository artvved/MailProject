using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Chunk
{
    public class ChunkManager : MonoBehaviour
    {
        [SerializeField] private Chunk[] chunkPrefabs;
        
        [SerializeField] private Chunk[] startChunks;
        
        private List<Chunk> chunks = new List<Chunk>();

        private int chunkCountToExist = 5;

        private void Start()
        {
            chunks.AddRange(startChunks);
        }

        public void UpdateChunks(Transform player)
        {
            if (chunks.Count<chunkCountToExist)
            {
                SpawnChunk(player);
            }if (chunks[0].gameObject.transform.position.z < player.position.z - 15)
            {
                DeleteChunk();
            }
        }
        
        private void DeleteChunk()
        {
            Destroy(chunks[0].gameObject);
            chunks.RemoveAt(0);
        }

        private void SpawnChunk(Transform player)
        {
           
            Chunk newChunk = Instantiate(GetRandomChunk(player));
            newChunk.transform.position = chunks[chunks.Count - 1].End.position - newChunk.Begin.localPosition;
            chunks.Add(newChunk);
        }
        private Chunk GetRandomChunk(Transform player)
        {
        
            List<float> chances = new List<float>();
            for (int i = 0; i < chunkPrefabs.Length; i++)
            {
                chances.Add(chunkPrefabs[i].ChanceFromDistance.Evaluate(player.transform.position.z));
            }

            float value = Random.Range(0, chances.Sum());
            float sum = 0;

            for (int i = 0; i < chances.Count; i++)
            {
                sum += chances[i];
                if (value < sum)
                {
                    return chunkPrefabs[i];
                }
            }

            return chunkPrefabs[chunkPrefabs.Length - 1];
            
        }
    }
}