using Game.Obstacles;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game.Chunk
{
    public class ObstaclePlacer : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Transform[] places;

        [SerializeField] private Obstacle[] obstaclePrefabs;

        private void Start()
        {
            for (int i = 0; i < places.Length; i++)
            {
                var tr = places[i];
                var obs=Instantiate(GetRandomObstacle(),root);
                obs.transform.position = tr.position;
                obs.transform.rotation = tr.rotation;
                Destroy(tr.gameObject);
            }
        }

        private Obstacle GetRandomObstacle()
        {
            return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        }
    }
}
