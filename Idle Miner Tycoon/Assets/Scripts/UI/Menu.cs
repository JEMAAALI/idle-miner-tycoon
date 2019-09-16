using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Menu : MonoBehaviour {


	public GameObject _menuBackground;
	public GameObject _popUp;
	public GameObject _map;
	public GameObject _text;
	public string _mineToSwitch;
	public GameObject _yesBtn;
	public GameObject _noBtn;
	public GameObject _saveBtn;
	public GameObject _backBtn;
	public GameObject _moneyAmount;


	void Start ()
	{
		
		if(GameObject.Find("DontDestroy").GetComponent<Show_Menu_OnLoadLevel>()._showMenu==1)
		{
			Time.timeScale=0.0f;
		}
		else
		{
			_menuBackground.SetActive(false);
			Time.timeScale=1.0f;
			this.GetComponent<Button>().interactable=true;
			_moneyAmount.SetActive(true);
			_menuBackground.SetActive(false);
			_popUp.SetActive(false);
			_map.SetActive(false);
			_text.GetComponent<Text>().text="";
			_yesBtn.SetActive(false);
			_noBtn.SetActive(false);

		}
	}

	//Show the menu
	public void ShowMenu()
	{
		Time.timeScale=0.0f;
		_menuBackground.SetActive(true);
		_backBtn.SetActive(true);
		_saveBtn.GetComponent<Button>().interactable=true;
		_popUp.SetActive(false);
		_map.SetActive(false);
		_text.GetComponent<Text>().text="";
		_yesBtn.SetActive(false);
		_noBtn.SetActive(false);
	}

	//Close the menu
	public void CloseMenu()
	{
		Time.timeScale=1.0f;
		_menuBackground.SetActive(false);
		_popUp.SetActive(false);
		_map.SetActive(false);
		_text.GetComponent<Text>().text="";
		_yesBtn.SetActive(false);
		_noBtn.SetActive(false);
	}

	//Close the popup 
	public void ClosePopUp()
	{
		_menuBackground.SetActive(true);
		_popUp.SetActive(false);
		_map.SetActive(false);
		_text.GetComponent<Text>().text="";
		_yesBtn.SetActive(false);
		_noBtn.SetActive(false);
	}

	//Start new game
	public void NewGame()
	{
		GameObject.Find("DontDestroy").GetComponent<Show_Menu_OnLoadLevel>()._showMenu=0;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		Time.timeScale=1.0f;
	}

	//Show the map
	public void ShowMap()
	{
		_map.SetActive(true);
		_menuBackground.SetActive(false);
		_popUp.SetActive(false);
		_text.GetComponent<Text>().text="";
		_yesBtn.SetActive(false);
		_noBtn.SetActive(false);
	}

	//Mine selection
	public void SwitchMines()
	{
		Scene scene = SceneManager.GetActiveScene();
		if(scene.name==_mineToSwitch)
		{
			_text.GetComponent<Text>().text="<color=#ff0000ff>“</color>"+_mineToSwitch+"<color=#ff0000ff>”</color> IS YOUR CURRENT MINE";
			_yesBtn.SetActive(false);
			_noBtn.SetActive(false);
		}
		else
		{
			_text.GetComponent<Text>().text="ARE YOU SURE YOU WANT TO SWITCH TO THE <color=#ff0000ff>“</color>"+_mineToSwitch+"<color=#ff0000ff>”</color> ?";
			_yesBtn.SetActive(true);
			_noBtn.SetActive(true);
		}
	}

	//Switch to the selected mine
	public void Yes()
	{
		GameObject.Find("DontDestroy").GetComponent<Show_Menu_OnLoadLevel>()._showMenu=1;
		SceneManager.LoadScene(_mineToSwitch);
	}


	public void No()
	{
		_text.GetComponent<Text>().text="";
		_yesBtn.SetActive(false);
		_noBtn.SetActive(false);
	}
}
