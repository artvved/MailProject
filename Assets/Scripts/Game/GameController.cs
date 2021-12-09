using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private PlayerController playerController;
    private MoveManager moveManager;
    

    void Start()
    {
        moveManager = new MoveManager(playerController.transform);
        inputController.SwipeEvent += (move =>
        {
            moveManager.Move(move);
        });
    }

    

    void Update()
        {
        }
    }