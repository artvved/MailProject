﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameScreen :MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image image;

       

        public void SetScoreText(int score)
        {
            scoreText.text= "Score : " + score;
        }

        public void SetScoreColor(Color32 color)
        {
            image.color = color;
            
        }
    }
}