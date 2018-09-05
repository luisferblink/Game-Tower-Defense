using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolArrows : MonoBehaviour {

	[HideInInspector]
	public List<GameObject> poolBullets = new List<GameObject>();

	public GameObject arrow;
	public int numberArrows;

	void Start(){
		for (int i = 0; i < numberArrows; i++) {
			GameObject b = Instantiate (arrow)as GameObject;
			b.SetActive (false);
			poolBullets.Add (b);
		}
	}

	public GameObject GetBullet(){
		if (poolBullets.Count > 0) {
			GameObject b = poolBullets [0];
			poolBullets.RemoveAt (0);
			b.SetActive (true);
			b.GetComponent<Arrow> ().Invoke ("ActiveCollider", 0.5f);
			return b;
		}
		return null;
	}

	public void SetBullet(GameObject b){
		poolBullets.Add (b);
		b.SetActive (false);
	}
}
