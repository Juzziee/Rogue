using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

	static public List<GameObject> roomList;
	static public List<GameObject> corridorList;

	public int numRooms;
	public int numCorridors;

	private int roomCount;
	private int corridorCount;

	public GameObject cubePrefab;

	// Use this for initialization
	void Start () {
		roomList = new List<GameObject> ();
		corridorList = new List<GameObject> ();
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

		//Debug.Log (roomList.Count);

	}


	void SeparateRooms(){

		GameObject roomA;
		GameObject roomB;

		float dx, dxa, dxb, dy, dya, dyb; 

		bool isTouching;

		do {
			isTouching = false;
			for (int i = 0; i < roomList.Count; i++){
				roomA = roomList[i].gameObject;
				for(int j = i+1; j < roomList.Count; j++){
					Debug.Log("Sep " + i + "." + j);
					roomB = roomList[j].gameObject;
					isTouching = roomA.GetComponent<Collider2D>().IsTouching(roomB.GetComponent<Collider2D>());
					if(isTouching){
						Vector2 roomABoundsMin = roomA.GetComponent<Collider2D>().bounds.min;
						Vector2 roomABoundsMax = roomA.GetComponent<Collider2D>().bounds.max;

						Vector2 roomBBoundsMin = roomB.GetComponent<Collider2D>().bounds.min;
						Vector2 roomBBoundsMax = roomB.GetComponent<Collider2D>().bounds.max;

						dx = Mathf.Min (roomABoundsMax.x - roomBBoundsMin.x + 1, roomABoundsMin.x - roomBBoundsMax.x - 1);
						dy = Mathf.Min (roomABoundsMin.y - roomBBoundsMax.y + 1, roomABoundsMax.y - roomBBoundsMin.y - 1);

						if (Mathf.Abs (dx) < Mathf.Abs (dy)) {
							dy = 0;
						} else { 
							dx = 0;
						}

						dxa = -dx / 2;
						dxb = dx + dxa;

						dya = -dy / 2;
						dyb = dy + dya;

						roomA.gameObject.transform.position = new Vector2 (dxa, dya);
						roomB.gameObject.transform.position = new Vector2 (dxb, dyb);
					}
				}
			}
		} while (isTouching);
			
	}

	void ConnectRooms() {

		double abDist, acDist, bcDist;


	}
}
