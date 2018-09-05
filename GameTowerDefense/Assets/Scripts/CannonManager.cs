using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

	public Transform spawnBullet, limitBullet;

	public float timeRate;
	public float distBullet;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;
	float tempRate;

	PoolCannonBullet enlacePoolCannonBullet;
	ProjectileAspect enlaceProjectileAspect;

	void Start(){
		enlacePoolCannonBullet = GameObject.FindObjectOfType<PoolCannonBullet> ();
		enlaceProjectileAspect = GameObject.FindObjectOfType<ProjectileAspect> ();
		limitBullet.localPosition = new Vector3 (0, 0, distBullet);
	}


	void Update(){
		tempRate += Time.deltaTime;
		if (tempRate >= timeRate) {
			tempRate = 0;
			StartCoroutine(ShootBullet(enlacePoolCannonBullet.GetBullet ().transform, limitBullet.position));
		}
	}

	public IEnumerator ShootBullet(Transform projectile, Vector3 target){
		projectile.position = spawnBullet.position;
		float target_Distance = Vector3.Distance(projectile.position, target);
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		float flightDuration = target_Distance / Vx;

		projectile.rotation = Quaternion.LookRotation(target - projectile.position);
		float elapse_time = 0;
		while (elapse_time < flightDuration){
			projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
			elapse_time += Time.deltaTime;
			yield return null;
		}
	}
}
