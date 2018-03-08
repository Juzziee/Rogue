using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

	public static int padding = -1;
	public static int mapSize = 35;
	public static int minSideLength = 5;
	public static int maxSideLength = 15;
	public static int corridorCount = 10;
	public static int roomCount = 80;
	public static double minRatio = 1;
	public static double maxRatio = 1.5;
	public static double touchedRoomChance = 1;

	static public List<Room> rooms, corridors, halls, untouched;

	public static Room start, end;

	public GameObject spritePrefab;



	//------------------

	public void Start(){
		generate ();
	}


	public void generate(){
		rooms = new List<Room> ();
		corridors = new List<Room> ();
		halls = new List<Room> ();
		untouched = new List<Room> ();

		createRooms ();
		separateRooms ();
	}

	private void createRooms(){
		int width, height, x, y;
		double ratio; 
		Room room; 

		for (int roomID = 0; roomID < roomCount; roomID++) {
			do {
				width = Random.Range (minSideLength, maxSideLength);
				height = Random.Range (minSideLength, maxSideLength);
				ratio = Room.getRatio (width, height);
			} while (ratio < minRatio || ratio > maxRatio);

			x = getRandomGausInt(mapSize * 2) - mapSize - width/2;
			y = getRandomGausInt(mapSize * 2) - mapSize - height/2;
			room = new Room(x, y, width, height);
			Debug.Log((roomID+1) + "/" + roomCount + " - Adding room " + room.toString());
			rooms.Add(room);
			roomID++;

			GameObject sprite = Instantiate (spritePrefab);
			sprite.transform.localScale = new Vector2 (width, height);
			sprite.transform.position = new Vector2 (x, y);
		}
	}


	private void separateRooms() {
		Room a, b;
		int dx, dxa, dxb, dy, dya, dyb;
		bool touching;
		int step = 1;
		do {
			Debug.Log("Seperating Rooms. Itteration: " + (step++));
			touching = false;
			for(int i = 0; i < rooms.Count; i++) {
				a = rooms[i];
				for(int j = i+1; j < rooms.Count; j++) {
					b = rooms[j];
					if(a.touches(b)) {
						Debug.Log("Touching");
						touching = true;
						dx = Mathf.Min(a.getRight()-b.getLeft()+padding, a.getLeft()-b.getRight()-padding);
						dy = Mathf.Min(a.getBottom()-b.getTop()+padding, a.getTop()-b.getBottom()-padding);
						if(Mathf.Abs(dx) < Mathf.Abs(dy)) dy = 0;
						else dx = 0;

						dxa = -dx/2;
						dxb = dx+dxa;

						dya = -dy/2;
						dyb = dy+dya;

						a.shift(dxa,  dya);
						b.shift(dxb,  dyb);
					}
				}
			}
		} while(touching);
	}

	private void findCorridors () {
		rooms.Sort (rooms);
		for (int count = 0; count < corridorCount; count++) {
			corridors.Add (rooms.Remove (0));
		}
	}


	private void centerCorridors(){

	}











	private int getRandomGausInt(int size) {
		double r = Random.Range(0, 1);
		r *= size/5;
		r += size/2;
		if(r < 0 || r > size)
			return getRandomGausInt(size);
		else
			return (int)r;
	}
}
