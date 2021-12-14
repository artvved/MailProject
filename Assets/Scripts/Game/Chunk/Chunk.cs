using Game.Obstacles;
using UnityEngine;

namespace Game.Chunk
{
   public class Chunk : MonoBehaviour
   {
      [SerializeField] private AnimationCurve chanceFromDistance;
      [SerializeField] private Transform begin;
      [SerializeField] private Transform end;
      
      [SerializeField] private Transform[] places;

      [SerializeField] private Obstacle[] obstaclePrefabs;
      [SerializeField] private bool isSingleColor;

      public AnimationCurve ChanceFromDistance => chanceFromDistance;

      public Transform Begin => begin;

      public Transform End => end;

      public Transform[] Places => places;

      public Obstacle[] ObstaclePrefabs => obstaclePrefabs;

      public bool IsSingleColor => isSingleColor;
   }
}
