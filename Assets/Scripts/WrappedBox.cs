using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappedBox : MonoBehaviour
{
    public string color;

    GameObject bottom;
    Transform cover, questionMark;
    List<Transform> walls =  new List<Transform>();
    
    Vector3 coverTargetPos = new Vector3(-2f, 0f, -3f);
    Vector3 coverTargetRot =  new Vector3(0, 40, 0);

    bool reveal;
    float coverSpeed = 4f;

    void Start()
    {
        bottom = transform.Find("Base").gameObject;
        cover = transform.Find("Cover");
        questionMark = transform.Find("QuestionMark");

        walls.Add(transform.Find("WallPivot1"));
        walls.Add(transform.Find("WallPivot2"));
        walls.Add(transform.Find("WallPivot3"));
        walls.Add(transform.Find("WallPivot4"));

        reveal = false;

        switch (color)
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

        if (Input.GetKeyDown(KeyCode.Space)) OpenBox();
    }

    void RotateQuestionMark()
    {
        questionMark.Rotate(0f, 0.1f, 0f);
    }

    public void OpenBox()
    {
        reveal = true;
    }

    void Reveal()
    {
        questionMark.gameObject.SetActive(false);

        foreach (Transform wall in walls)
        {
            if (wall.eulerAngles.x < 90) wall.Rotate(0.1f, 0, 0);
            else wall.eulerAngles = new Vector3(90, wall.eulerAngles.y, wall.eulerAngles.z);
        }

        Vector3 targetPos = new Vector3(coverTargetPos.x, cover.position.y, coverTargetPos.z);

        if (Vector3.Distance(cover.position, targetPos) <= 0.01f )
        {
            cover.position = 
                Vector3.MoveTowards(cover.position, coverTargetPos, coverSpeed * Time.deltaTime);
        }
        else
        {
            cover.position = 
                Vector3.MoveTowards(cover.position, targetPos, coverSpeed * Time.deltaTime);
        }

        if (cover.eulerAngles.y < 40) cover.Rotate(0f, 0.1f, 0f);
    }

    void SetColor(Color _color)
    {
        bottom.GetComponent<Renderer>().material.color = _color;
    }
}

