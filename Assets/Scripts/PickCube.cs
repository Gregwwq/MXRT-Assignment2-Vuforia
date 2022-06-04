using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour
{
    public bool Picked { get; set; }

    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        Picked = false;
    }

    void Update()
    {
        if (Picked)
        {
            transform.position = player.Find("PickUpSpot").position;
            transform.rotation = player.rotation;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.position.y + (transform.localScale.y / 2), transform.position.z);
        }
    }
}
