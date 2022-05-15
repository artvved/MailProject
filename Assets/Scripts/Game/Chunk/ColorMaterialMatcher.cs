using System;
using System.Collections.Generic;
using UnityEngine;
using Color = Game.Player.Color;


namespace Game.Chunk
{
    public class ColorMaterialMatcher : MonoBehaviour
    {
        [Header("Obstacle Materials")]
        [SerializeField] private Material greenObstacleMaterial;
        [SerializeField] private Material orangeObstacleMaterial;
        [SerializeField] private Material purpleObstacleMaterial;
        
        private Dictionary<Color,Material> colorObstaclePairs = new Dictionary<Color,Material>();
        
        [Header("Effect Materials")] 
        [SerializeField] private Material greenEffectMaterial;
        [SerializeField] private Material orangeEffectMaterial;
        [SerializeField] private Material purpleEffectMaterial;
        
        private Dictionary<Color,Material> colorEffectPairs = new Dictionary<Color,Material>();

        private void Start()
        {
            colorObstaclePairs.Add(Color.GREEN,greenObstacleMaterial);
            colorObstaclePairs.Add(Color.ORANGE,orangeObstacleMaterial);
            colorObstaclePairs.Add(Color.PURPLE,purpleObstacleMaterial);
            
            colorEffectPairs.Add(Color.GREEN,greenEffectMaterial);
            colorEffectPairs.Add(Color.ORANGE,orangeEffectMaterial);
            colorEffectPairs.Add(Color.PURPLE,purpleEffectMaterial);
        }

        public Material GetObstacleMaterial(Color color)
        {
            return colorObstaclePairs[color];
        }
        
        public Material GetEffectMaterial(Color color)
        {
            return colorEffectPairs[color];
        }


    }
}