using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour
{
    Transform player;

    bool holding;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        holding = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "PickCube")
                {
                    PickCube cubeScript = hit.transform.gameObject.GetComponent<PickCube>();
                    
                    if (!holding && !cubeScript.Picked && cubeScript.Pickable)
                    {
                        cubeScript.Picked = true;
                        holding = true;
                    }
                    else if (holding && cubeScript.Picked)
                    {
                        cubeScript.Picked = false;
                        holding = false;

                        hit.transform.position = new Vector3(player.Find("PickUpSpot").position.x, transform.position.y + (hit.transform.localScale.y / 2), player.Find("PickUpSpot").position.z);
                    } 
                }

                else if (hit.transform.gameObject.tag == "WrappedBox")
                {
                    WrappedBox script = hit.transform.gameObject.GetComponent<WrappedBox>();
                    if (!script.OpenAlr)
                    {
                        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

                        gm.AnsweringQuestion(script.BoxIndex);
                    }
                }
            }
        }
    }
}