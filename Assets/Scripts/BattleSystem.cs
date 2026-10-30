using UnityEngine;
using UnityEngine.UI;
using System.Collections;  
using System.Collections.Generic;
using TMPro;

//Battle State Checker
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    // References to the player and enemy prefabs, their units, HUDs, and spawn locations
    public GameObject playerPrefab;
    Unit playerUnit;
    public BattleHud playerHUD;
    public Transform playerSpawnLocation;
    public GameObject enemyPrefab;
    Unit enemyUnit;
    public BattleHud enemyHUD;
    public Transform enemySpawnLocation;

    // The current state of the battle
    public BattleState state;

    // Reference to the dialogue text UI element and the sub-battle menu
    public TextMeshProUGUI dialogueText;
    public GameObject SubBattleMenu;

    // Reference to anything item related
    public ItemsManager itemsManager;
    public Button itemButtonPrefab;

    public ItemsData[] itemCounts;

    void Start()
    {
        ResetItems();

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    public void ResetItems()
    {
        foreach (var item in itemCounts)
        {
            item.ResetItemCount();
        }
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnLocation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawnLocation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";
        dialogueText.text = "Gained a potion!";
        dialogueText.text = "Gained a Ether!";
        dialogueText.text = "Gained a Throwing Knife!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);


        //itemsManager.AddPotion();
        //itemsManager.AddEther();
        //itemsManager.AddThrowingKnife();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);        
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerMagic()
    {
        bool isDead = enemyUnit.TakeMagicDamage(playerUnit.maxMP);
        playerUnit.UpdateMP(5);

        playerHUD.SetMP(playerUnit.currentMP);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You used magic!";
        yield return new WaitForSeconds(2f);
        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerItem()
    {
        bool hasItems = itemsManager.itemCount > 0;

        if (hasItems)
        {
            SetupSubMenu();
            dialogueText.text = "Choose an item:";
        }
        else
        {
            dialogueText.text = "You have no items left!";
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            
        }
        
    }

    IEnumerator potionButton()
    {
        itemsManager.UsePotion(playerUnit);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You used a potion!";
        yield return new WaitForSeconds(2f);
        CloseSubMenu();
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator etherButton()
    {
        itemsManager.UseEther(playerUnit);
        playerHUD.SetMP(playerUnit.currentMP);
        dialogueText.text = "You used an ether!";
        yield return new WaitForSeconds(2f);
        CloseSubMenu();
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator throwingKnifeButton()
    {
        //bool isDead = itemsManager.UseThrowingKnife(enemyUnit);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You used a throwing knife!";
        yield return new WaitForSeconds(2f);
        CloseSubMenu();
        //if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        //else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnMagicButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerMagic());
    }

    public void OnItemButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerItem());
    }

    public void OnPotionItemChoice()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(potionButton());
    }

    public void OnEtherItemChoice()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(etherButton());
    }

    public void OnThrowingKnifeItemChoice()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(throwingKnifeButton());
    }

    public void SetupSubMenu()
    {
        SubBattleMenu.SetActive(true);

    }

    public void CloseSubMenu()
    {
        SubBattleMenu.SetActive(false);
    }
}
