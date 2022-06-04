using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 1.2f;
    public float RotateSpeed = 180f;

    Joystick js;

    Transform cam;

    void Start()
    {
        js = FindObjectOfType<Joystick>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        float hori = js.Horizontal;
        float vert = js.Vertical;

        if(hori != 0 || vert != 0)
        {
            Vector3 right = new Vector3(cam.right.x, 0, cam.right.z);
            Vector3 forward = new Vector3(cam.forward.x, 0, cam.forward.z);
            
            Vector3 move = ((hori * right) + (vert * forward)).normalized;
            move += transform.position;

            transform.position = Vector3.MoveTowards(transform.position, move, Speed * Time.deltaTime);

            Quaternion lookRot = Quaternion.LookRotation((move - transform.position), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, RotateSpeed * Time.deltaTime);
        }
    }
}