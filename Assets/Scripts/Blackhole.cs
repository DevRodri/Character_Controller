using UnityEngine;
using UnityEditor;
using System.Collections;



public class Blackhole : MonoBehaviour {
	
	public float time = 5.0f;
	public float radius = 10.0f;
	public float power = -100f; //implosion
	private GameObject particlehole;

	void Start () 
	{
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/mystical_01.prefab", typeof(GameObject));
		particlehole = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		particlehole.transform.position = gameObject.transform.position;
	}
	
	void Update () 
	{
		time -= Time.deltaTime; 

		if (time > 0) {
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
			foreach (Collider hit in colliders) {
					if ((hit) && (hit.rigidbody)) {
							hit.rigidbody.AddExplosionForce (power, explosionPos, radius, 3);
					}
			}
		} 
		else {
			Destroy (particlehole);
			Destroy (gameObject);

				}
		
	}
}


