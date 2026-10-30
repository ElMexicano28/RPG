using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsManager : MonoBehaviour
{
    public int itemCount;

    public List<string> itemNames = new List<string>();
    public VerticalLayoutGroup itemsMenu;
    public Button itemButtonPrefab;
    public BattleSystem BattleSystem;

    public List<ItemsData> itemsDataList = new List<ItemsData> ();

    public ItemsData potion;
    public ItemsData ether;
    public ItemsData elixer;
    public ItemsData fireSoulFragment;
    public ItemsData iceSoulFragment;
    public ItemsData lightningSoulFragment;
    public ItemsData rockSoulFragment;
    public ItemsData physicalSoulFragment;
    public ItemsData nonelementalSoulFragment;
    
    public void Start()
    {
        // Don't overwrite an Inspector-assigned reference.
        if (itemsMenu == null)
        {
            itemsMenu = GetComponent<VerticalLayoutGroup>() 
                        ?? GetComponentInChildren<VerticalLayoutGroup>();

            if (itemsMenu == null)
            {
                Debug.LogWarning("ItemsManager: VerticalLayoutGroup not found on this GameObject or its children. Assign itemsMenu in the Inspector.", this);
            }
        }


    }

    /*
    private void CreateItemButton(string itemName, int count)
    {
        if (itemButtonPrefab == null || itemsMenu == null)
            return;

        var button = Instantiate(itemButtonPrefab, itemsMenu.transform);
        button.gameObject.name = $"ItemButton_{itemName}";

        // Try to set the label text (support both UI.Text and TextMeshPro)
        
        var tmp = button.GetComponentInChildren<TMP_Text>();
        if (tmp != null)
            tmp.text = $"{itemName} x{count}";

        if(itemName == "Potion")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnPotionItemChoice);
        else if(itemName == "Ether")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnEtherItemChoice);
        else if(itemName == "Elixer")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Fire Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Ice Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Lighting Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Rock Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Physical Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
        else if(itemName == "Non-Elemental Soul Fragment")
            button.GetComponent<Button>().onClick.AddListener(BattleSystem.OnElixerItemChoice);
    }
    */

    public void AddPotion(int count)
    {   
        potion.AddItem(count);
        
        Debug.Log("Added a potion. Total potions: " + potion.itemCount);
    }

    public void UsePotion(Unit unit)
    {
        if (potion.itemCount > 0)
        {
            unit.Restore(potion.restoreHP);
            potion.UseItem();
            Debug.Log("Used a potion. Remaining potions: " + potion.itemCount);
            
        }
        else
        {
            Debug.Log("No potions left!");
            
        }
    }

    public bool HasPotions()
    {
        return potion.itemCount > 0;
    }

    public void AddEther()
    {
        if (ether.itemCount == 0)
        {
            ether.itemCount = 1;
            //CreateItemButton(ether.itemName, ether.itemCount);
            itemCount++;

        }
        else
        {
            ether.itemCount++;
            itemCount++;
        }
        
        Debug.Log("Added an ether. Total ethers: " + ether.itemCount);
    }

    public void UseEther(Unit unit)
    {
        if (ether.itemCount > 0)
        {
            unit.Restore(ether.restoreMP);
            ether.itemCount--;
            itemCount--;
            Debug.Log("Used an ether. Remaining ethers: " + ether.itemCount);

        }
        else
        {
            Debug.Log("No ethers left!");

        }
    }

    public bool HasEthers()
    {
        return ether.itemCount > 0;
    }

    
    
}
