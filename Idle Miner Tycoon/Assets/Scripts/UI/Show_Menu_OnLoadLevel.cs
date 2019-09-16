using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Menu_OnLoadLevel : MonoBehaviour {

	public int _showMenu = 1;
	 
	public void Awake()
	{
		DontDestroyOnLoad(this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			//Destroy any other gameObject with CheckMenu Class
			Destroy(gameObject);
		}
	}







}
