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
        [SerializeField] private bool isSingleColor;

        private void Start()
        {
            if (isSingleColor)
            {
                var obs = GetRandomObstacle();
                for (int i = 0; i < places.Length; i++)
                {
                    PlaceObstacle(places[i],obs);
                }
            }
            else
            {
                for (int i = 0; i < places.Length; i++)
                {
                   PlaceObstacle(places[i],GetRandomObstacle());
                }
            }
        }

        private Obstacle GetRandomObstacle()
        {
            return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        }

        private void PlaceObstacle(Transform tr,Obstacle obstacle)
        {
            var obs = Instantiate(obstacle, root);
            obs.transform.position = tr.position;
            obs.transform.rotation = tr.rotation;
            Destroy(tr.gameObject);
        }
    }
}