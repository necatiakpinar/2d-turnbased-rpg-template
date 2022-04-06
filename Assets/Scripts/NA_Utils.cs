using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NA_Utils : MonoBehaviour
{
    public static NA_Utils Instance;
    private void Awake() 
    {
        Instance = this;    
    }
}
