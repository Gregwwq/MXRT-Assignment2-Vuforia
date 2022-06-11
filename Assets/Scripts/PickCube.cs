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

        up = false;

        top = new Vector3(0f, 1.3f, 0f);
        bot = new Vector3(0f, 0.7f, 0f);
    }

    void Update()
    {
        if (Picked)
        {
            transform.position = player.Find("PickUpSpot").position;
            transform.rotation = player.rotation;
        }

        MoveArrow();

        float dist = Vector3.Distance(transform.position, player.position);
        Pickable = dist <= 1.2f ? true : false;

        if (Pickable && !Picked) arrow.gameObject.SetActive(true);
        else arrow.gameObject.SetActive(false);
    }

    void MoveArrow()
    {
        if (Vector3.Distance(arrow.localPosition, top) < 0.01f) up = false;
        if (Vector3.Distance(arrow.localPosition, bot) < 0.01f) up = true;

        if (up) arrow.localPosition = Vector3.MoveTowards(arrow.localPosition, top, 0.2f * Time.deltaTime);
        else arrow.localPosition = Vector3.MoveTowards(arrow.localPosition, bot, 0.2f * Time.deltaTime);

        arrow.Rotate(0f, 0.3f, 0f);
    }
}
