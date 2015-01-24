using UnityEngine;
using System.Collections;

public class BulletLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollision2D(Collision col){
		print ("tööt");
		if(col.gameObject.tag.Equals("Wall")){
			Destroy(gameObject);
		}
	}
}
