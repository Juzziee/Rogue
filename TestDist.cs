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

	bool touching;

	// Use this for initialization
	void Start () {

		RoomACol = roomA.GetComponent<Collider2D> ();
		RoomBCol = roomB.GetComponent<Collider2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		bool isTouching = RoomACol.IsTouching (RoomBCol);
		Debug.Log (RoomACol.IsTouching (RoomBCol));

		if (isTouching) {
			
			roomABoundsMin = RoomACol.bounds.min;
			roomABoundsMax = RoomACol.bounds.max;

			roomBBoundsMin = RoomBCol.bounds.min;
			roomBBoundsMax = RoomBCol.bounds.max;

			float xdist = roomABoundsMax.x - roomBBoundsMin.x;

			float dx = Mathf.Min (roomABoundsMax.x - roomBBoundsMin.x + 1, roomABoundsMin.x - roomBBoundsMax.x - 1);
			float dy = Mathf.Min (roomABoundsMin.y - roomBBoundsMax.y + 1, roomABoundsMax.y - roomBBoundsMin.y - 1);

			if (Mathf.Abs (dx) < Mathf.Abs (dy)) {
				dy = 0;
			} else { 
				dx = 0;
			}

			float dxa = -dx / 2;
			float dxb = dx + dxa;

			float dya = -dy / 2;
			float dyb = dy + dya;

			roomA.gameObject.transform.position = new Vector2 (dxa, dya);
			roomB.gameObject.transform.position = new Vector2 (dxb, dyb);
		}
	}
}
