using System;
using System.Collections.Generic;
using System.Linq;
using Game.Obstacles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Chunk
{
    public class ChunkController : MonoBehaviour
    {
        [SerializeField] private Chunk[] chunkPrefabs;
        
        [SerializeField] private Chunk startChunkPrefab;
        [SerializeField] private Transform root;
        
        private List<Chunk> chunks = new List<Chunk>();

        private int chunkCountToExist = 5;
        private float chunkDestroyDistance = 15f;

     
        public void SpawnStartChunks()
        {
           
            for (int i = 0; i < 3; i++)
            {
                
                SpawnChunk(startChunkPrefab);
            }
           
        }
        
        private void SetUpChunk(Chunk chunk)
        {
            var places = chunk.Places;
            if (chunk.IsSingleColor)
            {
                var obs = GetRandomObstacle(chunk.ObstaclePrefabs);
                for (int i = 0; i < places.Length; i++)
                {
                    PlaceObstacle(places[i],obs,chunk.transform);
                }
            }
            else
            {
                for (int i = 0; i < places.Length; i++)
                {
                    PlaceObstacle(places[i],GetRandomObstacle(chunk.ObstaclePrefabs),chunk.transform);
                }
            }
        }
        
        private Obstacle GetRandomObstacle(Obstacle[] obstaclePrefabs)
        {
            return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        }

        private void PlaceObstacle(Transform tr,Obstacle obstacle,Transform rootChunk)
        {
            var obs = Instantiate(obstacle,rootChunk );
            obs.transform.position = tr.position;
            obs.transform.rotation = tr.rotation;
            Destroy(tr.gameObject);
        }

        public void ClearChunks()
        {
            while (chunks.Count>0)
            {
                DeleteChunk();
            }
        }

        private void SpawnChunk(Chunk chunk)
        {
           
            Chunk newChunk = Instantiate(chunk,root);
            if (chunks.Count>0)
            {
                newChunk.transform.position = chunks[chunks.Count - 1].End.position - newChunk.Begin.localPosition;
            }
            else
            {
                var position = newChunk.transform.position;
                position =new Vector3( position.x, position.y, position.z-chunkDestroyDistance);
                newChunk.transform.position = position;
            }

            SetUpChunk(newChunk);
            chunks.Add(newChunk);
        }

        public void UpdateChunks(Transform player)
        {
            if (chunks.Count<chunkCountToExist)
            {
                SpawnChunk(GetRandomChunk(player));
            }if (chunks[0].gameObject.transform.position.z < player.position.z - chunkDestroyDistance)
            {
                DeleteChunk();
            }
        }

        private void DeleteChunk()
        {
            Destroy(chunks[0].gameObject);
            chunks.RemoveAt(0);
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