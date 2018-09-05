using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public enum stateEnemy
	{
		walk, attack, dead
	};

	public stateEnemy state;

	public float limitRay;
	public float life;

	private NavMeshAgent navMeshAgent;
	private Transform target;

	Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Tower").transform;
	}


	public void StartParameterEnemy(){
		state = stateEnemy.walk;
	}

	void Update(){
		if (state == stateEnemy.dead)
			return;
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection (Vector3.forward) * limitRay;
		Debug.DrawRay (transform.position, forward, Color.green);
		if (Physics.Raycast (transform.position, forward, out hit, limitRay)) {
			if (hit.collider.tag.Contains ("Tower")) {
				state = stateEnemy.attack;
			}
		}

		if (state == stateEnemy.walk) {
			navMeshAgent.SetDestination (target.position);
		}else if(state == stateEnemy.attack){
			navMeshAgent.isStopped = true;
			anim.SetBool ("Attack", true);
			anim.SetBool ("Walk", false);
		}
	}


	void OnTriggerEnter(Collider col){
		if (col.tag.Contains ("Arrow")) {
			if (life > 0) {
				life--;
			} else if (life <= 0) {
				state = stateEnemy.dead;
				GameObject.FindObjectOfType<PoolEnemy> ().SetEnemy (this.gameObject);
			}
		}
	}
}
