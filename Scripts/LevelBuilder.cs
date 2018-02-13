using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

	static public List<GameObject> roomList;

	public int numRooms;
	private int roomCount;

	public GameObject cubePrefab;

	// Use this for initialization
	void Start () {
		roomList = new List<GameObject> ();
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Generate(){
		for(roomCount = 0; roomCount <= numRooms; roomCount++){
			GameObject cube = Instantiate (cubePrefab);
			roomList.Add (cube);
			Vector2 spawnPosition = Random.insideUnitCircle * 40;
			int sizeX = Random.Range (4, 12);
			int sizeY = Random.Range (4, 8);

			cube.transform.localScale = new Vector3 (sizeX, sizeY, 1);
			cube.transform.position = spawnPosition;

		}

		Debug.Log (roomList.Count);

	}

	void SeparateRooms(){

	}
}
