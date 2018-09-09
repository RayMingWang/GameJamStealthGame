using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMark : MonoBehaviour
{
	public GameObject walkMark;
	public GameObject runMark;

	public void SwitchMark()
	{
		walkMark.SetActive(false);
		runMark.SetActive(true);
	}
}
