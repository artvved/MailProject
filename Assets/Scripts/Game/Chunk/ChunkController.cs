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
        [SerializeField] private Chunk[] chunkPrefabs;

        [SerializeField] private Chunk startChunkPrefab;
        [SerializeField] private Transform root;

        private List<Chunk> chunks = new List<Chunk>();

        private int chunkCountToExist = 5;
        private float chunkDestroyDistance = 15f;
        public ColorMaterialMatcher ColorMaterialMatcher { get; set; }

        private Color[] colors = new[] {Color.GREEN, Color.ORANGE, Color.PURPLE};


        public void SpawnStartChunks()
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnChunk(startChunkPrefab);
            }
        }

        private void SetUpChunk(Chunk chunk)
        {
            //setup position
            SetUpChunkPosition(chunk);
            //setup obstacles and effects
            SetUpChunkObstacles(chunk);
            SetUpChunkEffects(chunk);
        }

        private void SetUpChunkObstacles(Chunk chunk)
        {
            var obstacles = chunk.Places;
            if (chunk.IsSingleColor)
            {
                var color = GetRandomObstacleColor();
                var obsMat = ColorMaterialMatcher.GetObstacleMaterial(color);
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
                    var obsMat = ColorMaterialMatcher.GetObstacleMaterial(color);
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
                var obsMat = ColorMaterialMatcher.GetEffectMaterial(color);
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

        private Color GetRandomObstacleColor()
        {
            return colors[Random.Range(0, colors.Length)];
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

        public void UpdateChunks(Transform player)
        {
            if (chunks.Count < chunkCountToExist)
            {
                SpawnChunk(GetRandomChunk(player));
            }

            if (chunks[0].gameObject.transform.position.z < player.position.z - chunkDestroyDistance)
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