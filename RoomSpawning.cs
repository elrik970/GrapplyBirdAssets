using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpawningSpot;
    public LevelRooms[] Levels;
    // public LevelRooms[] SpawnedInLevels; 
    public int[] lenArray;
    public Color[] backgroundColors;
    public float AmountToSpawn;
    public Transform Player;
    public int levelIndex = 0;
    private float oldSpawningSpot;
    public Camera Camera;
    private Color curBgColor;
    public float backgroundChangeSpeed;

    void Start()
    {
        levelIndex = Random.Range(0,backgroundColors.Length-1);

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (Player != null) {
            Player = playerObject.transform;
        }
        
        Camera = Camera.main;

        curBgColor = backgroundColors[levelIndex];


    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) {

            if (Player.position.x > SpawningSpot-60) {
                SpawnRooms();
            }

            curBgColor = backgroundColors[levelIndex];

            Camera.backgroundColor = Color.Lerp(Camera.backgroundColor,curBgColor,Time.deltaTime*backgroundChangeSpeed);
        }
        if (Player == null) {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null) {
                Player = playerObject.transform;
            }
        }
    }
    void SpawnRooms() {
        for (int i = 0; i < AmountToSpawn; i++) {
            int index = Random.Range(0,Levels[levelIndex].Rooms.Length);
            GameObject RoomtoSpawnObject = Levels[levelIndex].Rooms[index];
            Room RoomtoSpawn = RoomtoSpawnObject.GetComponent<Room>();
            SpawningSpot+=RoomtoSpawn.HorizontalSize;

            GameObject.Instantiate(RoomtoSpawnObject,new Vector3(SpawningSpot-(RoomtoSpawn.HorizontalSize/2),0,0),Quaternion.identity);

            // if (SpawnedInLevels[levelIndex].Rooms[index] == null) {
                // SpawnedInLevels[levelIndex].Rooms[index] = (GameObject)
            // }
            // else {
            //     SpawnedInLevels[levelIndex].Rooms[index].transform.position = new Vector3(SpawningSpot-(RoomtoSpawn.HorizontalSize/2),0,0);
            // }

            if (lenArray[levelIndex] < SpawningSpot-oldSpawningSpot-60) {
                levelIndex++;

                if (levelIndex > Levels.Length-1) {
                    levelIndex = 0;
                }

                curBgColor = backgroundColors[levelIndex];

                oldSpawningSpot = SpawningSpot;


            }

        }

        
    }
}

[System.Serializable]
public struct LevelRooms {
    [SerializeField] public GameObject[] Rooms;
}
