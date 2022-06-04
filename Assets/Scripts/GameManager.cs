using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject WrappedBoxPrefab, GreenCubePrefab, BlueCubePrefab, RedCubePrefab;

    List<GameObject> WrappedBoxes = new List<GameObject>();
    Transform player;

    void Awake()
    {
        LoadPrefabs();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
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
                cube.position.z > minZ && cube.position.y < maxZ
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
        Vector3 pos1 = new Vector3(player.position.x - 3, player.position.y, player.position.z);
        Vector3 pos2 = new Vector3(player.position.x, player.position.y, player.position.z + 3);
        Vector3 pos3 = new Vector3(player.position.x + 3, player.position.y, player.position.z);

        GameObject greenCube = Instantiate(GreenCubePrefab, pos1, Quaternion.identity);
        GameObject blueCube = Instantiate(BlueCubePrefab, pos2, Quaternion.identity);
        GameObject redCube = Instantiate(RedCubePrefab, pos3, Quaternion.identity);

        GameObject wrappedBoxRed = Instantiate(WrappedBoxPrefab, pos1, Quaternion.identity);
        GameObject wrappedBoxGreen = Instantiate(WrappedBoxPrefab, pos2, Quaternion.identity);
        GameObject wrappedBoxBlue = Instantiate(WrappedBoxPrefab, pos3, Quaternion.identity);

        WrappedBoxes.Add(wrappedBoxRed);
        WrappedBoxes.Add(wrappedBoxGreen);
        WrappedBoxes.Add(wrappedBoxBlue);

        wrappedBoxRed.GetComponent<WrappedBox>().BaseColor = "Red";
        wrappedBoxRed.GetComponent<WrappedBox>().WantedCube = redCube;

        wrappedBoxGreen.GetComponent<WrappedBox>().BaseColor = "Green";
        wrappedBoxGreen.GetComponent<WrappedBox>().WantedCube = greenCube;

        wrappedBoxBlue.GetComponent<WrappedBox>().BaseColor = "Blue";
        wrappedBoxBlue.GetComponent<WrappedBox>().WantedCube = blueCube;
    }
}
