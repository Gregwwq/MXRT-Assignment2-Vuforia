using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractions : MonoBehaviour
{
    bool holding;

    void Start()
    {
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
                    
                    if (!holding && !cubeScript.Picked)
                    {
                        cubeScript.Picked = true;
                        holding = true;
                    }
                    else if (holding && cubeScript.Picked)
                    {
                        cubeScript.Picked = false;
                        holding = false;
                    } 
                }

                if (hit.transform.gameObject.tag == "WrappedBox")
                {
                    
                }
            }
        }
    }
}