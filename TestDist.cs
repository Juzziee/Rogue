using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDist : MonoBehaviour {

	public GameObject roomA;
	public GameObject roomB;

	public Vector2 roomABoundsMin;
	public Vector2 roomABoundsMax;

	public Vector2 roomBBoundsMin;
	public Vector2 roomBBoundsMax;

	private Collider2D RoomACol;
	private Collider2D RoomBCol;

	// Use this for initialization
	void Start () {

		RoomACol = roomA.GetComponent<Collider2D> ();
		RoomBCol = roomB.GetComponent<Collider2D> ();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		roomABoundsMin = RoomACol.bounds.min;
		roomABoundsMax = RoomACol.bounds.max;

		roomBBoundsMin = RoomBCol.bounds.min;
		roomBBoundsMax = RoomBCol.bounds.max;

		float xdist = roomABoundsMax.x - roomBBoundsMin.x;

		float dx = Mathf.Min (roomABoundsMax.x - roomBBoundsMin.x, roomABoundsMin.x - roomBBoundsMax.x);
		Debug.Log (dx);
	}
}
