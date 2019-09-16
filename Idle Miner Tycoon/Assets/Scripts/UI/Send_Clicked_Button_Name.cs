using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send_Clicked_Button_Name : MonoBehaviour {

	public GameObject _menuBtn;

	public void Send () 
	{
		//Set the name of the scene depending on the selected mine of the map.
		Menu gett = _menuBtn.GetComponent<Menu>();
		gett._mineToSwitch = this.gameObject.name;
		gett.SwitchMines();
	}
}
