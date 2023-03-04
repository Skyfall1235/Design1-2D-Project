using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollX : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private int stageNum;
    [SerializeField] private bool move = false;
    public bool Move
    {
        get {return move;}
        set {move = value;}
    }
    
    void Start()
    {
        stageNum = 0;
    }

    void Update()
    {
        if(move)
        {
            target = waypoints[stageNum].transform;
            cam.transform.position = Vector3.Lerp(cam.transform.position, target.position, speed * Time.deltaTime);
        } else {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<PlayerMovement>().isFacingR)
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                stageNum += 1;
                move = true;

            } else if (!player.GetComponent<PlayerMovement>().isFacingR) {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                stageNum -= 1;
                move = true;
            }
        }
    }
}
