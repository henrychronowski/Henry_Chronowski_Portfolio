using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Henry Chronowski
 * Purpose: Built for camera shake, can be applied to any object to induce shake
 * 
 * Requirements: Object must have a parent object with a transform component
 * 
 */

public class CameraShake : MonoBehaviour
{
	[Tooltip("Speed of the shake")]
	[SerializeField]
	private float frequency = 25;
	[Tooltip("Maximum translational offset")]
	[SerializeField]
	Vector3 maxTranslation = Vector3.one * .5f; //was .5
	[Tooltip("Maximum angular offset")]
	[SerializeField]
	Vector3 maxAngular = Vector3.one * 2;
	[Tooltip("Length of recovery from shake")]
	[SerializeField]
	float recoverySpeed = 1.5f;
	[Tooltip("Curves the reduction of shake: 1 is linear, 2 is squared, etc")]
	[SerializeField]
	float traumaExponent = 2;

#if UNITY_EDITOR
	[SerializeField]
	bool triggerShake = false;
#endif

	/* In order to have scaled shake from a target on the camera (think explosion, etc) put this code in the script triggering the shake.
	 * This will scale the shake to be larger when it's closer to the camera
	 * 
	 *[SerializeField]
	 *float range = 45;
	 * 
	 *.//In trigger function:
	 *float distance = Vector3.Distance(Transform.position, < Camera >.transform.position);
	 * <Camera>.InduceStress(1 - distance);
	 */

	private float seed;
	private float trauma = 0f;

	private void Awake()
	{
		seed = Random.value;
	}

	private void Update()
	{
        
#if UNITY_EDITOR
		if(triggerShake)
		{
			InduceTrauma(1.0f);
			triggerShake = false;
		}
#endif

		float shake = Mathf.Pow(trauma, traumaExponent);
		//transform.localPosition = new Vector3(
			//maxTranslation.x * Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1,
			//maxTranslation.y * Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1,
			//maxTranslation.z * Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1
			//) * shake;

		transform.localRotation = Quaternion.Euler(new Vector3(
			maxAngular.x * Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1,
			maxAngular.y * Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1,
			maxAngular.z * Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1
			) * shake);

		trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
        
	}

	// Triggers shake, clamped to [0 1]
	public void InduceTrauma(float trauma)
	{
		this.trauma = Mathf.Clamp01(this.trauma + trauma);
	}
}
