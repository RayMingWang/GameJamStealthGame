using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
	[Header("Thunder Setting")]
	public AnimationCurve[] lightningSequence;
	public AudioClip thunderSoundEffect;
	public float thunderVolume = 1.5f;
	public float thunderDelay=0.9f;

	[Header("Lightning Setting")]
	public float lightningTensity = 1f;
	
	private Light _light;
	private bool _playingLightning = false;
    private bool _prepareLightning = false;
	private float _lightningTime = 0f;
	private AudioSource _audioSource;

	private AnimationCurve _selectedSequence;
	// Use this for initialization
	private void Start ()
	{
		_light = GetComponent<Light>();
		_light.enabled = false;

		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	private void Update () {
		if (!_prepareLightning)
		{
            _prepareLightning = true;
            StartCoroutine("RandomThunder");
            
           
           
		}
		if(_playingLightning)
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
			StartCoroutine("PlayThunder");
            _prepareLightning = false;
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
				_light.intensity = lightningTensity*sequence.Evaluate(offsets);
			}
		}
	}

	IEnumerator PlayThunder()
	{
		yield return new WaitForSeconds(thunderDelay);
		_audioSource.PlayOneShot(thunderSoundEffect,thunderVolume);
	}

    IEnumerator RandomThunder()
    {
          yield return new WaitForSeconds(Random.Range(0,5f));

        _playingLightning = true;
        _selectedSequence = lightningSequence[Random.Range(0, lightningSequence.Length)];
        _lightningTime = 0;
        SetLightning(_lightningTime, _selectedSequence);
    }

}
