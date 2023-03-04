using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    [SerializeField] private GameObject scrollBox;
    
    void OnTriggerEnter2D()
    {
        scrollBox.GetComponent<ScrollX>().Move = false;
    }
}
