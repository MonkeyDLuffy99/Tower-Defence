using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	[SerializeField] float movementPeriod = 0.5f;
	[SerializeField] ParticleSystem goalParticle;

	// Use this for initialization
	void Start () {
		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		var path = pathfinder.GetPath();
		StartCoroutine(FollowPath(path));
	}

	IEnumerator FollowPath(List<Waypoint> path) {
		foreach(Waypoint waypoint in path) {
			Vector3 correctedPosition = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z);
			transform.position = correctedPosition;
			yield return new WaitForSeconds(movementPeriod);
		}
		SelfDestruct();
	} 

	void SelfDestruct() {
		transform.position = new Vector3(transform.position.x, transform.position.y+14, transform.position.z);
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        Destroy(gameObject);
    }
	
}
