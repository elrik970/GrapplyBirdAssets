using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    public float SpawningSpot;
    public LevelRooms[] Levels;
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
        Player = GameObject.FindWithTag("Player").transform;
        
        Camera = Camera.main;

        curBgColor = backgroundColors[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x > SpawningSpot-60) {
            SpawnRooms();
        }

        Camera.backgroundColor = Color.Lerp(Camera.backgroundColor,curBgColor,Time.deltaTime*backgroundChangeSpeed);
    }
    void SpawnRooms() {
        for (int i = 0; i < AmountToSpawn; i++) {
            GameObject RoomtoSpawnObject = Levels[levelIndex].Rooms[Random.Range(0,Levels[levelIndex].Rooms.Length)];
            Room RoomtoSpawn = RoomtoSpawnObject.GetComponent<Room>();
            SpawningSpot+=RoomtoSpawn.HorizontalSize;
            GameObject.Instantiate(RoomtoSpawnObject,new Vector3(SpawningSpot-(RoomtoSpawn.HorizontalSize/2),0,0),Quaternion.identity);

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
