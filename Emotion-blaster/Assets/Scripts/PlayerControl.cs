using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerControl : MonoBehaviour {

	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 12;
	public float jumpHeight = 12;

	public GameObject thing;
	public GameObject cursor;

	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}

		targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		currentSpeed = IncrementTowards (currentSpeed, targetSpeed, acceleration);

		if (playerPhysics.grounded) {
			amountToMove.y = 0;

			if(Input.GetButtonDown("Jump")) {
				amountToMove.y = jumpHeight;
			}
		}

		if(Input.GetButtonDown("Fire1")) {
			Vector3 direction = cursor.transform.position - transform.position;
			if (direction.magnitude > 1) {
				direction = direction.normalized;
			}
			GameObject newBullet = (GameObject) Instantiate(thing, transform.position + direction, Quaternion.identity);

			newBullet.rigidbody2D.AddForce (direction * 500);
		}
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move (amountToMove * Time.deltaTime);
	}

	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}

}
