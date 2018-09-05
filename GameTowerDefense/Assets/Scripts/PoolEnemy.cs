using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour {

	[HideInInspector]
	public List<GameObject> poolEnemies = new List<GameObject>();

	public GameObject enemy;
	public int numberEnemies;

	void Start(){
		for (int i = 0; i < numberEnemies; i++) {
			GameObject b = Instantiate (enemy)as GameObject;
			b.SetActive (false);
			poolEnemies.Add (b);
		}
	}

	public GameObject GetEnemy(){
		if (poolEnemies.Count > 0) {
			GameObject b = poolEnemies [0];
			poolEnemies.RemoveAt (0);
			b.SetActive (true);
			b.GetComponent<Enemy> ().StartParameterEnemy ();
			return b;
		}
		return null;
	}

	public void SetEnemy(GameObject b){
		poolEnemies.Add (b);
		b.SetActive (false);
	}
}
