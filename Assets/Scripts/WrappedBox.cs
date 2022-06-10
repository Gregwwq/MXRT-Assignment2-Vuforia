using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappedBox : MonoBehaviour
{
    [HideInInspector]
    public string BaseColor;
    [HideInInspector]
    public GameObject WantedCube;
    [HideInInspector]
    public bool Completed;

    [HideInInspector]
    public string Question;
    [HideInInspector]
    public string[] Prompts;
    [HideInInspector]
    public int Answer;

    GameObject bottom, glow;
    Transform cover, questionMark;
    List<Transform> walls =  new List<Transform>();
    
    Vector3 coverTargetPos = new Vector3(-2f, 0f, -3f);
    Vector3 coverTargetRot =  new Vector3(0, 40, 0);

    bool reveal, alreadyGlow;

    float elap = 0;
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
        alreadyGlow = false;

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
        RotateQuestionMark();

        if (reveal) Reveal();

        if (elap > 1)
        {
            OpenBox();
        }
        else elap += Time.deltaTime;

        if (Completed)
        {
            if (!alreadyGlow)
            {
                glow.transform.position = new Vector3(transform.position.x, transform.position.y - 8, transform.position.z);
                alreadyGlow = true;
            }
            glow.SetActive(true);
        }
        else
        {
            glow.SetActive(false);
            alreadyGlow = false;
        }

        glow.transform.position = Vector3.MoveTowards(glow.transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z), 6 * Time.deltaTime);
    }

    void RotateQuestionMark()
    {
        questionMark.Rotate(0f, 0.1f, 0f);
    }

    public void OpenBox()
    {
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

