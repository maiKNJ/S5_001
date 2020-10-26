using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	[SerializeField] private Vector3 speed = Vector3.zero;

	void Update ()
	{
		transform.rotation *= Quaternion.Euler(speed * Time.deltaTime);
	}
}
