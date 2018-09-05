using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAspect : MonoBehaviour {

	public LayerMask layer;

	public Transform crossHair;

	public float firingAngle = 45.0f;
	public float gravity = 9.8f;
	    
	private Transform myTransform;

	PoolArrows enlaceBulletsManager;

	void Awake(){
		myTransform = transform;      
	}

	void Start(){
		enlaceBulletsManager = GameObject.FindObjectOfType<PoolArrows> ();
	}
		
	void Update(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layer)) {
			if (hit.collider != null && hit.collider.tag.Contains("Floor")) {
				crossHair.position = new Vector3 (hit.point.x, crossHair.position.y, hit.point.z);
				if (Input.GetMouseButtonDown (0) && enlaceBulletsManager.poolBullets.Count > 0) {
					LaunchArrow (enlaceBulletsManager.GetBullet ().transform, hit.point);
				}
			}
		}
	}

	void LaunchArrow(Transform projectile, Vector3 target){
		projectile.position = new Vector3(0, 0, 0);
		Vector3 pos = projectile.position;

		float dist = Vector3.Distance(pos, target);

		projectile.LookAt(target);

		float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * firingAngle * 2)));
		float Vy, Vz;

		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * firingAngle);
		Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * firingAngle);

		Vector3 localVelocity = new Vector3(0f, Vy, Vz);

		Vector3 globalVelocity = projectile.TransformVector(localVelocity);

		projectile.GetComponent<Rigidbody>().velocity = globalVelocity;
	}
}
