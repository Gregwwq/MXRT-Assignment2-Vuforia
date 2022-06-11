using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject WrappedBoxPrefab, GreenCubePrefab, BlueCubePrefab, RedCubePrefab;
    GameObject joystick, qnPanel;

    Text question, prompt1, prompt2, prompt3;

    List<GameObject> WrappedBoxes = new List<GameObject>();

    Transform player, plane;

    bool setupComplete;
    int selectedBoxIndex;

    string[] questions = {
        "What time is our hotel's breakfast buffet available",
        "How much does our dinner buffet cost",
        "What is the time period for guests to check in and check out from their rooms"
    };
    string[][] prompts = {
        new string[] {
            "6am - 10am",
            "6am - 12pm", 
            "8am - 12pm"
        },
        new string[] {
            "$45",
            "$10", 
            "$70"
        },
        new string[] {
            "6am - 11am",
            "11am - 2pm", 
            "10am - 1pm"
        }
    };
    int[] answers = {3, 1, 2};

    void Awake()
    {
        LoadPrefabs();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
        plane = GameObject.Find("MainPlane").transform;
        joystick = GameObject.Find("Joystick");
        qnPanel = GameObject.Find("QnPanel");

        joystick.SetActive(false);
        qnPanel.SetActive(false);

        question = GameObject.Find("Question").GetComponent<Text>();
        prompt1 = GameObject.Find("Prompt1").GetComponentInChildren<Text>();
        prompt2 = GameObject.Find("Prompt2").GetComponentInChildren<Text>();
        prompt3 = GameObject.Find("Prompt3").GetComponentInChildren<Text>();

        plane.position = player.position;

        setupComplete = false;

        SetupScene();
    }

    void Update()
    {
        foreach (GameObject box in WrappedBoxes)
        {
            Transform bottom = box.transform.Find("Base");
            Transform cube = box.GetComponent<WrappedBox>().WantedCube.transform;

            float minX = bottom.position.x - (bottom.localScale.x / 2);
            float maxX = bottom.position.x + (bottom.localScale.x / 2);
            float minZ = bottom.position.z - (bottom.localScale.z / 2);
            float maxZ = bottom.position.z + (bottom.localScale.z / 2);

            if (
                cube.position.x > minX && cube.position.x < maxX
                &&
                cube.position.z > minZ && cube.position.z < maxZ
            )
            {
                box.GetComponent<WrappedBox>().Completed = true;
            }
            else 
            {
                box.GetComponent<WrappedBox>().Completed = false;
            }
        }
    }

    void LoadPrefabs()
    {
        WrappedBoxPrefab = (GameObject) Resources.Load("Prefabs/WrappedBox", typeof(GameObject));
        GreenCubePrefab = (GameObject) Resources.Load("Prefabs/GreenCube", typeof(GameObject));
        BlueCubePrefab = (GameObject) Resources.Load("Prefabs/BlueCube", typeof(GameObject));
        RedCubePrefab = (GameObject) Resources.Load("Prefabs/RedCube", typeof(GameObject));
    }

    public void SetupScene()
    {
        if (!setupComplete)
        {
            Vector3 pos1 = new Vector3(player.position.x - 4, player.position.y, player.position.z);
            Vector3 pos2 = new Vector3(player.position.x, player.position.y, player.position.z + 4);
            Vector3 pos3 = new Vector3(player.position.x + 4, player.position.y, player.position.z);

            GameObject wrappedBoxRed = Instantiate(WrappedBoxPrefab, pos1, Quaternion.identity);
            GameObject wrappedBoxGreen = Instantiate(WrappedBoxPrefab, pos2, Quaternion.identity);
            GameObject wrappedBoxBlue = Instantiate(WrappedBoxPrefab, pos3, Quaternion.identity);

            Vector3 cubePos1 = new Vector3(player.position.x - 4, player.position.y + (GreenCubePrefab.transform.localScale.y / 2), player.position.z);
            Vector3 cubePos2 = new Vector3(player.position.x, player.position.y + (BlueCubePrefab.transform.localScale.y / 2), player.position.z + 4);
            Vector3 cubePos3 = new Vector3(player.position.x + 4, player.position.y + (RedCubePrefab.transform.localScale.y / 2), player.position.z);

            GameObject greenCube = Instantiate(GreenCubePrefab, cubePos1, Quaternion.identity);
            GameObject blueCube = Instantiate(BlueCubePrefab, cubePos2, Quaternion.identity);
            GameObject redCube = Instantiate(RedCubePrefab, cubePos3, Quaternion.identity);

            WrappedBoxes.Add(wrappedBoxRed);
            WrappedBoxes.Add(wrappedBoxGreen);
            WrappedBoxes.Add(wrappedBoxBlue);

            wrappedBoxRed.GetComponent<WrappedBox>().BaseColor = "Red";
            wrappedBoxRed.GetComponent<WrappedBox>().WantedCube = redCube;
            wrappedBoxRed.GetComponent<WrappedBox>().BoxIndex = 0;

            wrappedBoxGreen.GetComponent<WrappedBox>().BaseColor = "Green";
            wrappedBoxGreen.GetComponent<WrappedBox>().WantedCube = greenCube;
            wrappedBoxGreen.GetComponent<WrappedBox>().BoxIndex = 1;

            wrappedBoxBlue.GetComponent<WrappedBox>().BaseColor = "Blue";
            wrappedBoxBlue.GetComponent<WrappedBox>().WantedCube = blueCube;
            wrappedBoxBlue.GetComponent<WrappedBox>().BoxIndex = 2;

            wrappedBoxRed.transform.parent = plane;
            wrappedBoxGreen.transform.parent = plane;
            wrappedBoxBlue.transform.parent = plane;

            greenCube.transform.parent = plane;
            blueCube.transform.parent = plane;
            redCube.transform.parent = plane;

            joystick.SetActive(false);

            setupComplete = true;
        }
    }

    public void AnsweringQuestion(int index)
    {   
        selectedBoxIndex = index;

        SetQuestion();

        joystick.SetActive(false);
        qnPanel.SetActive(true);
    }

    void SetQuestion()
    {
        question.text = questions[selectedBoxIndex];
        prompt1.text = prompts[selectedBoxIndex][0];
        prompt2.text = prompts[selectedBoxIndex][1];
        prompt3.text = prompts[selectedBoxIndex][2];
    }

    public void Answer1()
    {
        GameObject selectedBox = WrappedBoxes[selectedBoxIndex];
        WrappedBox script = selectedBox.GetComponent<WrappedBox>();

        if (answers[script.BoxIndex] == 1) script.OpenBox();
        else
        {
            joystick.SetActive(true);
            qnPanel.SetActive(false);
        }
    }

    public void Answer2()
    {
        GameObject selectedBox = WrappedBoxes[selectedBoxIndex];
        WrappedBox script = selectedBox.GetComponent<WrappedBox>();

        if (answers[script.BoxIndex] == 2) script.OpenBox();
        else
        {
            joystick.SetActive(true);
            qnPanel.SetActive(false);
        }
    }

    public void Answer3()
    {
        GameObject selectedBox = WrappedBoxes[selectedBoxIndex];
        WrappedBox script = selectedBox.GetComponent<WrappedBox>();

        if (answers[script.BoxIndex] == 3) script.OpenBox();
        else
        {
            joystick.SetActive(true);
            qnPanel.SetActive(false);
        }
    }
}
