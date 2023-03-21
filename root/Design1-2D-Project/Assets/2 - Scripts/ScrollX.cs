using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollX : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private Transform waypoint;
    private float speed = 5;
    [SerializeField] private bool move = false;
    
    void Update()
    {
        if(move)
        {
            if (cam.transform.position != waypoint.position)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, waypoint.position, speed * Time.deltaTime);
            }
        }
    }

    // Make combat lock code and collider
    
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            move = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            move = false;
        }
    }
}
