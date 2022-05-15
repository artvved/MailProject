using System.Collections.Generic;
using Game.Obstacles;
using UnityEngine;

namespace Game.Chunk
{
   public class Chunk : MonoBehaviour
   {
      [SerializeField] private AnimationCurve chanceFromDistance;
      [SerializeField] private Transform begin;
      [SerializeField] private Transform end;
      
      [SerializeField] private Obstacle[] places;
      [SerializeField] private ParticleSystem[] laserSparkEffectPlaces;
     


      [SerializeField] private bool isSingleColor;

      public AnimationCurve ChanceFromDistance => chanceFromDistance;

      public Transform Begin => begin;

      public Transform End => end;

      public Obstacle[] Places => places;

      public ParticleSystem[] LaserSparkEffectPlaces => laserSparkEffectPlaces;
     
      public bool IsSingleColor => isSingleColor;

     
   }
}
