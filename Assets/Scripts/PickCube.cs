using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour
{
    public bool Picked { get; set; }
    public bool Pickable { get; private set; }

    Transform player, arrow;

    Vector3 top, bot;
    bool up;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        arrow = transform.Find("Arrow");

        Picked = false;
        Pickable = true;

        top = new Vector3(arrow.position.x, arrow.position.y + 0.15f, arrow.position.z);
        bot = new Vector3(arrow.position.x, arrow.position.y - 0.15f, arrow.position.z);

        up = false;
    }

    void Update()
    {
        MoveArrow();

        float dist = Vector3.Distance(transform.position, player.position);
        Pickable = dist <= 1.2f ? true : false;

        if (Pickable) arrow.gameObject.SetActive(true);
        else arrow.gameObject.SetActive(false);

        if (Picked)
        {
            transform.position = player.Find("PickUpSpot").position;
            transform.rotation = player.rotation;
        }
    }

    void MoveArrow()
    {
        if (Vector3.Distance(arrow.position, top) < 0.01f) up = false;
        if (Vector3.Distance(arrow.position, bot) < 0.01f) up = true;

        if (up) arrow.position = Vector3.MoveTowards(arrow.position, top, 0.2f * Time.deltaTime);
        else arrow.position = Vector3.MoveTowards(arrow.position, bot, 0.2f * Time.deltaTime);

        arrow.Rotate(0f, 0.3f, 0f);
    }
}
