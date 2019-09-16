using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SaveLoad: MonoBehaviour {

	[SerializeField] private FinanceManager _managers;
	[SerializeField] private Inventory _elevatorDropOff_Amount;
	[SerializeField] private TextMeshProUGUI moneyText;
	[SerializeField] private TextMeshProUGUI _purchaseText;
	[SerializeField] private UpgradeActorUI _elevatorUpgrade;
	[SerializeField] private Actor _elevator;
	[SerializeField] private TextMeshProUGUI _UpgradeBtn_Text;
	[SerializeField] private TextMeshProUGUI _elevatorUpgradeBtn_Text;
	[SerializeField] private Actor _wareHouse;
	[SerializeField] private UpgradeActorUI _warehouseUpgrade;
	[SerializeField] private TextMeshProUGUI _warehouseUpgradeBtn_Text;
	public GameObject _menuBtn;
	public GameObject _menuBackground;
	public GameObject _popUp;
	public GameObject _map;
	private string[] _textSplit;
	public GameObject _moneyAmount;


 	void Start () 
	{
		// this array should be filled before you can use EncryptedPlayerPrefs :
		EncryptedPlayerPrefs.keys=new string[5];
		EncryptedPlayerPrefs.keys[0]="23Wrudre";
		EncryptedPlayerPrefs.keys[1]="SP9DupHa";
		EncryptedPlayerPrefs.keys[2]="frA5rAS3";
		EncryptedPlayerPrefs.keys[3]="tHat2epr";
		EncryptedPlayerPrefs.keys[4]="jaw3eDAs";

		//Get the name of the loaded scene and split it
		Scene scene = SceneManager.GetActiveScene(); 
		_textSplit = scene.name.Split(" "[0]);
 	}

	 
	 

	public void save()
	{
		FinanceManager _totalMoney = _managers.GetComponent<FinanceManager>();

		ShaftManager gett = _managers.GetComponent<ShaftManager>();

		Inventory _elevator_amount = _elevatorDropOff_Amount.GetComponent<Inventory>();

		UpgradeActorUI _elevatorUpgradeAmount = _elevatorUpgrade.GetComponent<UpgradeActorUI>();
		Actor _elevatorSkillMultiplier = _elevator.GetComponent<Actor>();

		UpgradeActorUI _wareHouseAmount = _warehouseUpgrade.GetComponent<UpgradeActorUI>();
		Actor _wareHouseSkillMultiplier = _wareHouse.GetComponent<Actor>();

		// we add the _textSplit[0] to the name of the saved value to ensure 
		// that the script can be used for saving a lot of mines.
		// Encrypt & Save the total number of current shafts. 
		EncryptedPlayerPrefs.SetInt(_textSplit[0]+"NumberOfShafts", +gett.Shafts.Count);

		// Encrypt & Save the totalMoney value. 
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"TotalMoney", +(float)_totalMoney.totalMoney);

		// Encrypt & Save the Elevator Drop Off Amount. 
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"ElevatorDropOff_Amount", +_elevator_amount.money);

		// Encrypt & Save the Elevator Upgrade Amount. 
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"ElevatorUpgradeAmount", +_elevatorUpgradeAmount._price);

		// Encrypt & Save the Elevator Skill Multiplier value.
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"ElevatorSkillMultiplier", +_elevatorSkillMultiplier.SkillMultiplier);

		// Encrypt & Save the WareHouse Amount.
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"WareHouseAmount", +_wareHouseAmount._price);

		// Encrypt & Save the WareHouse Skill Multiplier value.
		EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"WareHouseSkillMultiplier", +_wareHouseSkillMultiplier.SkillMultiplier);

		// Encrypt & Save the Upgrade Amout, Skill Multiplier & Drop Off Amount
		// for every shaft.
		for(int i=0; i<gett.Shafts.Count; i++)
		{
			float UpgradeAmount = gett.Shafts[i].transform.GetChild(3).GetChild(1).GetComponent<UpgradeActorUI>()._price;
			float SkillMultiplier = gett.Shafts[i].transform.GetChild(1).GetComponent<Actor>().SkillMultiplier;
			float DropOff_Amount = gett.Shafts[i].transform.GetChild(2).GetChild(1).GetComponent<Inventory>().money;

			EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"UpgradeAmount"+i, UpgradeAmount);
			EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"SkillMultiplier"+i, SkillMultiplier);
			EncryptedPlayerPrefs.SetFloat(_textSplit[0]+"DropOff_Amount"+i, DropOff_Amount);
		}

		// When the game is saved, hide the menu & return to game.
		_menuBackground.SetActive(false);
		Time.timeScale=1.0f;
 	}








	public void Load()
	{
		// We use StartCoroutine because when load the shafts with instantiating them 
		// we cannot directly access their variables and replace them with the saved values.  
		StartCoroutine(Loading());
	}

	IEnumerator Loading()
	{
		FinanceManager _totalMoney = _managers.GetComponent<FinanceManager>();

		// Check if we have any data saved before
		if(EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"TotalMoney", -1f)!=-1f)
		{
			
		// Get the totalMoney stored value and assign it.	
		double total = (double)EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"TotalMoney", -1f);
		_totalMoney.totalMoney= total;
		moneyText.text =""+total.ToString("F2"); 

		// Get the Elevator Drop Off Amount stored value and assign it.		
		Inventory _elevator_amount = _elevatorDropOff_Amount.GetComponent<Inventory>();
		_elevator_amount.money = EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"ElevatorDropOff_Amount", -1f);
		_elevator_amount._floatingText.text=""+Mathf.Round(_elevator_amount.money).ToString();

		// Get the Elevator Upgrade Amount stored value and assign it.	
		UpgradeActorUI _elevatorUpgradeAmount = _elevatorUpgrade.GetComponent<UpgradeActorUI>();
		_elevatorUpgradeAmount._price = EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"ElevatorUpgradeAmount", -1f);
		Actor _elevatorSkillMultiplier = _elevator.GetComponent<Actor>();
		
		// Get the Elevator Skill Multiplier stored value and assign it.	
		_elevatorSkillMultiplier.SkillMultiplier = EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"ElevatorSkillMultiplier", -1f);
		_elevatorUpgradeBtn_Text.text = ""+Mathf.Round(_elevatorUpgradeAmount._price).ToString(); 
		_elevatorUpgradeAmount.LoadLastUpgrade();
        
		// Get the WareHouse Amount stored value and assign it.	
		UpgradeActorUI _wareHouseAmount = _warehouseUpgrade.GetComponent<UpgradeActorUI>();
		_wareHouseAmount._price = EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"WareHouseAmount", -1f);

		// Get the WareHouse Skill Multiplier stored value and assign it.	
		Actor _wareHouseSkillMultiplier = _wareHouse.GetComponent<Actor>();
		_wareHouseSkillMultiplier.SkillMultiplier = EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"WareHouseSkillMultiplier", -1f);
		_warehouseUpgradeBtn_Text.text = ""+Mathf.Round(_wareHouseAmount._price).ToString(); 
		_wareHouseAmount.LoadLastUpgrade();

		// Get the total number of shafts saved value.	
		int nb_shafts = EncryptedPlayerPrefs.GetInt(_textSplit[0]+"NumberOfShafts", -1);
        
		// Load all the saved shafts.	
		for (int i=_managers.GetComponent<ShaftManager>().Shafts.Count; i<nb_shafts;i++)
		{
		    _managers.GetComponent<ShaftManager>().LoadPreviousShafts();
		}
        
		yield return 1;

		// Get	the Upgrade Amout, Skill Multiplier & Drop Off Amount stored value
		// for every shaft and assign them.
		ShaftManager gett = _managers.GetComponent<ShaftManager>();
		for (int i=0; i<nb_shafts;i++)
		{
			
			float UpgradeAmount = +EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"UpgradeAmount"+i, -1f);
			float SkillMultiplier = +EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"SkillMultiplier"+i, -1f);
			float DropOff_Amount = +EncryptedPlayerPrefs.GetFloat(_textSplit[0]+"DropOff_Amount"+i, -1f);

			gett.Shafts[i].transform.GetChild(1).GetComponent<Actor>().SkillMultiplier=SkillMultiplier;
			gett.Shafts[i].transform.GetChild(3).GetChild(1).GetComponent<UpgradeActorUI>()._price=UpgradeAmount;
			gett.Shafts[i].transform.GetChild(3).GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text =""+Mathf.Round(UpgradeAmount).ToString(); 
			gett.Shafts[i].transform.GetChild(3).GetChild(1).GetComponent<UpgradeActorUI>().LoadLastUpgrade();
			gett.Shafts[i].transform.GetChild(2).GetChild(1).GetComponent<Inventory>().money=DropOff_Amount;
			gett.Shafts[i].transform.GetChild(2).GetChild(1).GetComponent<Inventory>()._floatingText.text=""+Mathf.Round(DropOff_Amount).ToString();


		}

		// The game is loaded so, hide the menu & return to game.
		_popUp.SetActive(false);
		_menuBackground.SetActive(false);
		_map.SetActive(false);
		_menuBtn.GetComponent<Button>().interactable=true;
		_moneyAmount.SetActive(true);
		Time.timeScale=1.0f;
	    }

		// If no data was saved we show an alert message: There is no saved data to load 
		else
		{
		 _popUp.SetActive(true);
	     _menuBackground.SetActive(false);
		 _map.SetActive(false);
		}

	}














}
