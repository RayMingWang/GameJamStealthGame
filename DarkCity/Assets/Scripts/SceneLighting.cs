using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLighting : MonoBehaviour
{
	

	private Light _light;
	// Use this for initialization
	private void Start ()
	{
		_light = GetComponent<Light>();
		_light.enabled = false;
		
		
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown(KeyCode.X))
		{

			_light.enabled = !_light.enabled;

		}
	}
}
