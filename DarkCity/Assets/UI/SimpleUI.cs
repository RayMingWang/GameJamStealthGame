using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUI : MonoBehaviour
{

	public GameObject StartScreen;

	public GameObject DieScreen;

	public void PlayerKilled()
	{
		DieScreen.SetActive(true);
	}
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			StartScreen.SetActive(false);
		}
		
	}
}
