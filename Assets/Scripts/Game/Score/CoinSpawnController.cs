using UnityEngine;

namespace Game
{
    public class CoinSpawnController : MonoBehaviour
    {
        [SerializeField] private CoinView prefab;

        public CoinView SpawnCoinToPlace(Transform place)
        {
            return Instantiate(prefab, place);
        }

        public bool CanSpawn(float z)
        {

            if (prefab.ChanceCurve.Evaluate(z) > Random.Range(0, 1f))
            {
                return true;
            }
            else return false;
            
        }
    }
}