using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{

	public AnimationCurve[] lightningSequence;
	
	private Light _light;
	private bool _playingLightning = false;
	private float _lightningTime = 0f;

	private AnimationCurve _selectedSequence;
	// Use this for initialization
	private void Start ()
	{
		_light = GetComponent<Light>();
		_light.enabled = false;
	}
	
	// Update is called once per frame
	private void Update () {
		if (!_playingLightning)
		{
			if (Input.GetKeyDown("space"))
			{
				_selectedSequence = lightningSequence[Random.Range(0, lightningSequence.Length)];
				_lightningTime = 0;
				_playingLightning = true;
				SetLightning(_lightningTime,_selectedSequence);

			}
		}
		else
		{
			_lightningTime += Time.deltaTime;
			SetLightning(_lightningTime,_selectedSequence);
		}
		
	}

	private void SetLightning(float offsets, AnimationCurve sequence)
	{
		float endtime = sequence.keys[sequence.length - 1].time;
		if (offsets>endtime)
		{
			_playingLightning = false;
			_light.enabled = false;
			Debug.Log("Lightning end!");
		}
		else
		{
			if (sequence.Evaluate(offsets)<=0)
			{
				_light.enabled = false;
			}
			else
			{
				_light.enabled = true;
				_light.intensity = sequence.Evaluate(offsets);
			}
		}
	}

}
