using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollX : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private Transform target;
    public float speed;
    [SerializeField] private int stageNum;
    private bool move = false;
    
    void Start()
    {
        stageNum = 0;
    }

    void Update()
    {
        if(move)
        {
            target = waypoints[stageNum].transform;
            if (camera.transform.position != target.position)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, target.position, speed * Time.deltaTime);
            } else {
                move = false;
            }
        }
    }

    // bugs if a player jitters in the trigger exit
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<PlayerMovement>().isFacingR)
            {
                stageNum += 1;
                move = true;

            } else if (!player.GetComponent<PlayerMovement>().isFacingR) {
                stageNum -= 1;
                move = true;
            }
        }
    }
}
