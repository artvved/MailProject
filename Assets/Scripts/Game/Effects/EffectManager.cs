using System;
using UnityEngine;

namespace Game
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem matchEffectPrefab;
        private ParticleSystem matchEffect;
        
        [SerializeField] private Material orangeEffect;
        [SerializeField] private Material greenEffect;
        [SerializeField] private Material purpleEffect;
        
        private ParticleSystemRenderer renderer;

        private void Start()
        {
            matchEffect=Instantiate(matchEffectPrefab);
            renderer = matchEffect.GetComponent<ParticleSystemRenderer>();
        }

        public void PlayMatchEffect(Vector3 position, Color obstacleColorRequirement)
        {
            SetMaterial(obstacleColorRequirement);
            matchEffect.transform.position = position;
            matchEffect.Play();
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