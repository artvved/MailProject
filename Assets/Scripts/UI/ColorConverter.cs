using System.Collections.Generic;
using UnityEngine;
using Color = Game.Player.Color;

namespace UI
{
    public class ColorConverter
    {
        public Dictionary<Color, UnityEngine.Color32> Colors { get; }

        public ColorConverter()
        {
            Colors = new Dictionary<Color, UnityEngine.Color32>();
            Colors.Add(Color.PURPLE,new Color32(65 ,8 ,75,255));
            Colors.Add(Color.GREEN,new UnityEngine.Color32(18 ,75 ,22,255));
            Colors.Add(Color.ORANGE,new UnityEngine.Color32(122 ,63, 15,255));
        }

    }
    
    
    
}