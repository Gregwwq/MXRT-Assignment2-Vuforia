using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappedBox : MonoBehaviour
{
    public GameObject WantedCube { get; set; }
    public string BaseColor { get; set; }
    public bool Completed { get; set; }
    public int BoxIndex { get; set; }

    public bool OpenAlr { get; private set; }

    GameObject bottom, glow;
    Transform cover, questionMark;
    List<Transform> walls =  new List<Transform>();

    bool reveal;
    float rotatedAngle;

    void Start()
    {
        bottom = transform.Find("Base").gameObject;
        cover = transform.Find("Cover");
        questionMark = transform.Find("QuestionMark");
        glow = transform.Find("Glow").gameObject;

        walls.Add(transform.Find("WallPivot1"));
        walls.Add(transform.Find("WallPivot2"));
        walls.Add(transform.Find("WallPivot3"));
        walls.Add(transform.Find("WallPivot4"));

        reveal = false;
        Completed = false;
        OpenAlr = false;

        rotatedAngle = 0f;

        switch (BaseColor)
        {
            case "Red":
                SetColor(Color.red);
                break;

            case "Green":
                SetColor(Color.green);
                break;

            case "Blue":
                SetColor(Color.blue);
                break;
        }
    }

    void Update()
    {
        OpenBox();
        RotateQuestionMark();

        if (reveal) Reveal();

        if (Completed)
        {
            if (glow.transform.localScale.y < 1)
            {
                Vector3 grow = new Vector3(
                    glow.transform.localScale.x,
                    glow.transform.localScale.y + 0.01f,
                    glow.transform.localScale.z
                );
                glow.transform.localScale = grow;
            }
        }
        else
        {
            glow.transform.localScale = new Vector3(1f, 0f, 1f);
        }
    }

    void RotateQuestionMark()
    {
        questionMark.Rotate(0f, 0.2f, 0f);
    }

    public void OpenBox()
    {
        OpenAlr = true;
        reveal = true;

        questionMark.gameObject.SetActive(false);
        cover.gameObject.SetActive(false);
    }

    void Reveal()
    {
        foreach (Transform wall in walls)
        {
            if (rotatedAngle < 90f * 4f)
            {
                wall.Rotate(1f, 0, 0);
                rotatedAngle += 1f;
            } 
        }
    }

    void SetColor(Color _color)
    {
        bottom.GetComponent<Renderer>().material.color = _color;
    }
}

