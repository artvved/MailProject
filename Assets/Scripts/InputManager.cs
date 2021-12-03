using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<float,float> KeyPressedEvent;
    
    void Update()
    {
        var v=Input.GetAxis("Vertical");
        var h=Input.GetAxis("Horizontal");
        
        if (v !=0 || h !=0)
        {
            KeyPressedEvent?.Invoke(v,h);
        }
        
    }
}
