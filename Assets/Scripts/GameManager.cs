using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject WrappedBoxPrefab, GreenCubePrefab, BlueCubePrefab, RedCubePrefab;

    List<GameObject> WrappedBoxes = new List<GameObject>();
    Transform player, plane;

    bool setupComplete;

    void Awake()
    {
        LoadPrefabs();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
        plane = GameObject.Find("MainPlane").transform;

        plane.position = player.position;

        setupComplete = false;
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

            wrappedBoxGreen.GetComponent<WrappedBox>().BaseColor = "Green";
            wrappedBoxGreen.GetComponent<WrappedBox>().WantedCube = greenCube;

            wrappedBoxBlue.GetComponent<WrappedBox>().BaseColor = "Blue";
            wrappedBoxBlue.GetComponent<WrappedBox>().WantedCube = blueCube;

            wrappedBoxRed.transform.parent = plane;
            wrappedBoxGreen.transform.parent = plane;
            wrappedBoxBlue.transform.parent = plane;

            greenCube.transform.parent = plane;
            blueCube.transform.parent = plane;
            redCube.transform.parent = plane;

            setupComplete = true;
        }
    }
}
