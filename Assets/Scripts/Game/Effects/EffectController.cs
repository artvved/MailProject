using DG.Tweening;
using Game.Player;
using UnityEngine;
using Color = Game.Player.Color;

namespace Game.Effects
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem matchEffectPrefab;
        private ParticleSystem matchEffect;
        [SerializeField] private ParticleSystem mismatchEffectPrefab;
        private ParticleSystem mismatchEffect;
        
        [SerializeField] private Material orangeEffect;
        [SerializeField] private Material greenEffect;
        [SerializeField] private Material purpleEffect;
        
        private ParticleSystemRenderer renderer;

        private void Start()
        {
            matchEffect=Instantiate(matchEffectPrefab);
            mismatchEffect=Instantiate(mismatchEffectPrefab);
            renderer = matchEffect.GetComponent<ParticleSystemRenderer>();
        }

        public void PlayMatchEffect(PlayerView playerView, Vector3 position, Color obstacleColorRequirement)
        {
            Vector3 scale = playerView.transform.localScale;
            playerView.transform.DOScale(scale*1.1f,0.15f)
                .SetEase(Ease.InOutSine)
                .SetLoops(2,LoopType.Yoyo);
            SetMaterial(obstacleColorRequirement);
            PlayEffect(position,matchEffect);
        }
        public void PlayMismatchEffect(Vector3 position)
        {
           PlayEffect(position,mismatchEffect);
        }

        private void PlayEffect(Vector3 pos,ParticleSystem effect)
        {
            effect.transform.position = pos;
            effect.Play();
        }

        private void SetMaterial(Color color)
        {
            switch (color)
            {
                case Color.ORANGE:
                    renderer.material = orangeEffect;
                    break;
                case Color.GREEN:
                    renderer.material = greenEffect;
                    break;
                case Color.PURPLE:
                    renderer.material = purpleEffect;
                    break;
            }
        }
    }
}