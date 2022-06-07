using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 1.5f;
    public float RotateSpeed = 250f;

    Joystick js;

    Transform cam, plane;

    void Start()
    {
        js = FindObjectOfType<Joystick>();
        cam = Camera.main.transform;
        plane = GameObject.Find("MainPlane").transform;
    }

    void Update()
    {
        float hori = js.Horizontal;
        float vert = js.Vertical;

        if(hori != 0 || vert != 0)
        {
            Vector3 move1Right = new Vector3(cam.right.x, plane.right.y, cam.right.z);
            Vector3 move1Forward = new Vector3(cam.forward.x, plane.forward.y, cam.forward.z);

            Vector3 move1 = ((hori * -move1Right) + (vert * -move1Forward)).normalized;            

            move1 += plane.position;

            plane.position = Vector3.MoveTowards(plane.position, move1, Speed * Time.deltaTime);


            Vector3 move2Right = new Vector3(cam.right.x, plane.right.y, cam.right.z);
            Vector3 move2Forward = new Vector3(cam.forward.x, plane.forward.y, cam.forward.z);

            Vector3 move2 = ((hori * move2Right) + (vert * move2Forward)).normalized;

            move2 += transform.position;

            Quaternion lookRot = Quaternion.LookRotation((move2 - transform.position), plane.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, RotateSpeed * Time.deltaTime);
        }
    }
}