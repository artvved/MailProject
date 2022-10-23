using UnityEngine;

namespace Game
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private AnimationCurve chanceCurve;
        
        public AnimationCurve ChanceCurve => chanceCurve;
       
    }
}