using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        inputManager.KeyPressedEvent += (f, f1) =>
        {
            playerController.GetComponent<Rigidbody>().AddForce(10f*new Vector3(f1,f,0));
        };
    }
}
