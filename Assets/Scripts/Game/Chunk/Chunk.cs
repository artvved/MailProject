using UnityEngine;

namespace Game.Chunk
{
   public class Chunk : MonoBehaviour
   {
      [SerializeField] private AnimationCurve animationCurve;
      [SerializeField] private Transform begin;
      [SerializeField] private Transform end;

      public AnimationCurve ChanceFromDistance => animationCurve;

      public Transform Begin => begin;

      public Transform End => end;
   }
}
