using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour {

	public GameObject explosion;

	void Start(){
		
	}
	

	void Update(){
		
	}

	void OnCollisionEnter(Collision col){
		explosion.SetActive (true);
		explosion.GetComponent<ParticleSystem> ().Play ();
		GetComponent<SphereCollider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
		Invoke ("DesapearArrow", 2.0f);
	}

	void DesapearArrow(){
		CancelInvoke ("DesapearArrow");
		explosion.SetActive (false);
		GetComponent<SphereCollider> ().enabled = true;
		GetComponent<MeshRenderer> ().enabled = true;
		GameObject.FindObjectOfType<PoolCannonBullet> ().SetBullet (this.gameObject);
	}
}
