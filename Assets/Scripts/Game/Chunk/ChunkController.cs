using System;
using System.Collections.Generic;
using System.Linq;
using Game.Obstacles;
using UnityEngine;
using Color = Game.Player.Color;
using Random = UnityEngine.Random;

namespace Game.Chunk
{
    public class ChunkController : MonoBehaviour
    {
        [Header("Material Matcher")] [SerializeField]
        private ColorMaterialMatcher colorMaterialMatcher;

        [SerializeField] private CoinSpawnController coinSpawnController;

        [SerializeField] private Chunk[] chunkPrefabs;
        [SerializeField] private Chunk startChunkPrefab;
        [SerializeField] private Transform root;

        //chunks
        private List<Chunk> chunks = new List<Chunk>();
        private int chunkCountToExist = 5;

        private float chunkDestroyDistance = 15f;

        //decreasing coeff
        private Dictionary<Chunk, float> chunkDecreaseSpawnChanceMap;

        private Transform playerTransform;

        //obstacle colors
        private Color[] colors = new[] {Color.GREEN, Color.ORANGE, Color.PURPLE};


        public void Init(Transform player)
        {
            this.playerTransform = player;
            chunkDecreaseSpawnChanceMap = new Dictionary<Chunk, float>();
            foreach (var prefab in chunkPrefabs)
            {
                chunkDecreaseSpawnChanceMap.Add(prefab, 1f);
            }
        }

        public void SpawnStartChunks()
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnChunk(startChunkPrefab);
            }
        }

        private void SetUpChunk(Chunk chunk)
        {
            
            SetUpChunkPosition(chunk);
            SetUpChunkCoins(chunk);
            SetUpChunkObstacles(chunk);
            SetUpChunkEffects(chunk);
        }
        private void SetUpChunkCoins(Chunk chunk)
        {
            if (chunk.CoinPlaces.Length!=0)
            {
                if (coinSpawnController.CanSpawn(playerTransform.position.z))
                {
                    coinSpawnController.SpawnCoinToPlace(chunk.CoinPlaces[Random.Range(0, chunk.CoinPlaces.Length)]);
                }
            }
            
        }

        private void SetUpChunkObstacles(Chunk chunk)
        {
            var obstacles = chunk.Places;
            if (chunk.IsSingleColor)
            {
                var color = GetRandomObstacleColor();
                var obsMat = colorMaterialMatcher.GetObstacleMaterial(color);
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i].ColorRequirement = color;
                    obstacles[i].GetComponentInChildren<MeshRenderer>().material = obsMat;
                }
            }
            else
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    var color = GetRandomObstacleColor();
                    var obsMat = colorMaterialMatcher.GetObstacleMaterial(color);
                    obstacles[i].ColorRequirement = color;
                    obstacles[i].GetComponentInChildren<MeshRenderer>().material = obsMat;
                }
            }
        }

        private void SetUpChunkEffects(Chunk chunk)
        {
            var obstacles = chunk.Places;
            var laserSparkEffectPlaces = chunk.LaserSparkEffectPlaces;
            for (int i = 0; i < obstacles.Length; i++)
            {
                var color = obstacles[i].ColorRequirement;
                var obsMat = colorMaterialMatcher.GetEffectMaterial(color);
                laserSparkEffectPlaces[i].GetComponent<Renderer>().material = obsMat;
            }
        }

        private void SetUpChunkPosition(Chunk chunk)
        {
            if (chunks.Count > 0)
            {
                chunk.transform.position = chunks[chunks.Count - 1].End.position - chunk.Begin.localPosition;
            }
            else
            {
                var position = chunk.transform.position;
                position = new Vector3(position.x, position.y, position.z - chunkDestroyDistance);
                chunk.transform.position = position;
            }
        }


        public void ClearChunks()
        {
            while (chunks.Count > 0)
            {
                DeleteChunk();
            }
        }

        private void SpawnChunk(Chunk chunk)
        {
            Chunk newChunk = Instantiate(chunk, root);
            SetUpChunk(newChunk);
            chunks.Add(newChunk);
        }

        public void UpdateChunks()
        {
            if (chunks.Count < chunkCountToExist)
            {
                var rnd = GetRandomChunk();
                SpawnChunk(rnd);
                
                //reset decrease
                for (int j = 0; j < chunkPrefabs.Length; j++)
                {
                    chunkDecreaseSpawnChanceMap[chunkPrefabs[j]] =1f;
                }
                //set decrease for prev spawned chunk
                chunkDecreaseSpawnChanceMap[rnd] =2f ;
            }

            if (chunks[0].gameObject.transform.position.z < playerTransform.position.z - chunkDestroyDistance)
            {
                DeleteChunk();
            }
        }

        private void DeleteChunk()
        {
            Destroy(chunks[0].gameObject);
            chunks.RemoveAt(0);
        }

        private Color GetRandomObstacleColor()
        {
            return colors[Random.Range(0, colors.Length)];
        }

        private Chunk GetRandomChunk()
        {
            List<float> chances = new List<float>();
            for (int i = 0; i < chunkPrefabs.Length; i++)
            {
                var newChance = chunkPrefabs[i].ChanceFromDistance.Evaluate(
                                    playerTransform.position.z)/ chunkDecreaseSpawnChanceMap[chunkPrefabs[i]];
                chances.Add(newChance);
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