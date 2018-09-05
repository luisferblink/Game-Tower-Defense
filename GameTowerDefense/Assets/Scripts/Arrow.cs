using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	Rigidbody rigidbodyArrow;

	private Quaternion rot;
	private bool _rotate = true;

	void Start(){
		rigidbodyArrow = GetComponent<Rigidbody> ();
		Invoke ("ActiveCollider", 0.5f);
	}

	public void ActiveCollider(){
		CancelInvoke ("ActiveCollider");
		GetComponent<SphereCollider> ().enabled = true;
	}

	void Update(){
		if (_rotate) {
			transform.rotation = Quaternion.LookRotation (rigidbodyArrow.velocity);
			rot = transform.rotation;
		} else {
			transform.rotation = rot;
		}
	}

	void OnCollisionEnter(Collision col){
		rigidbodyArrow.isKinematic = true;
		_rotate = false;
		Invoke ("DesapearArrow", 1.0f);
	}

	void DesapearArrow(){
		CancelInvoke ("DesapearArrow");
		rigidbodyArrow.isKinematic = false;
		transform.rotation = Quaternion.identity;
		_rotate = true;
		GetComponent<SphereCollider> ().enabled = false;
		GameObject.FindObjectOfType<PoolArrows> ().SetBullet (this.gameObject);
	}
}
