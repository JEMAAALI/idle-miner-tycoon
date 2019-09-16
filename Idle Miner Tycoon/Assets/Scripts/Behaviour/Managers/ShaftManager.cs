using System.Collections.Generic;
using UnityEngine;
 
public class ShaftManager : MonoBehaviour
{
    [SerializeField] private FinanceManager financeManager;
    [SerializeField] private Actor elevator;
    [SerializeField] private GameSettings settings;
    [SerializeField] public GameObject shaftPrefab;
 
    public int MaxShafts;

    public List<Shaft> Shafts;

 

    public void BuildNextShaft()
    {
        var position = Shafts[Shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        
        var shaft = newObject.GetComponent<Shaft>();
        Shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, financeManager, Shafts.Count);

        financeManager.UpdateMoney(-financeManager.NextShaftPrice);
        financeManager.NextShaftPrice *= settings.ShaftIncrement;
    }

	// This function is called when we load the saved shafts
	// We dont have to Update the money cause we are loading the game.
	public void LoadPreviousShafts()
	{
		var position = Shafts[Shafts.Count - 1].NextShaftTransform.position;
		var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
		var shaft = newObject.GetComponent<Shaft>();
		Shafts.Add(shaft);
		shaft.ShaftManager = this;
		shaft.Initialize(elevator, financeManager, Shafts.Count);

		financeManager.NextShaftPrice *= settings.ShaftIncrement;
 		 
	}
}