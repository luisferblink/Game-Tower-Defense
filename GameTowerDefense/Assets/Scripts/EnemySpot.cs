using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpot : MonoBehaviour {

	public Transform tower;
	public float delaySpawn;
	public float Fangle;

	PoolEnemy enlaceEnemy;

	void Start(){
		enlaceEnemy = GameObject.FindObjectOfType<PoolEnemy> ();
		InvokeRepeating ("SpawnEnemies", Random.Range(0.0f, delaySpawn), Random.Range(0.0f, delaySpawn));
	}


	void Update(){
		transform.RotateAround (tower.position,Vector3.up,Fangle * Time.deltaTime);
	}

	void SpawnEnemies(){
		if (enlaceEnemy.poolEnemies.Count > 0) {
			GameObject tempEnemy = enlaceEnemy.GetEnemy ();
			tempEnemy.transform.position = transform.position;
		}
	}
}
