using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Lost, Win }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public GameObject enemy;

    public Transform enemySpawn;
    public Transform playerSpawn;

    public Text dialogue;

    Fighter playerChar;
    Fighter enemyChar;

    public BattleUI playerUI;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        SetupBattle();
    }

    public void SetupBattle()
    {
        GameObject playerO = Instantiate(player, playerSpawn);
        playerChar = playerO.GetComponent<Fighter>();
        GameObject enemyO = Instantiate(enemy, enemySpawn);
        enemyChar = enemyO.GetComponent<Fighter>();

        dialogue.text = "Prepare to fight!";

        playerUI.SetUI(playerChar);

        state = BattleState.PlayerTurn;
        Invoke("YourTurn", 2f);
    }
    
    public void YourTurn()
    {
        dialogue.text = "It's your turn!";
    }

    IEnumerator PlayerAttack()
    {
        bool isdead = enemyChar.TakeDamage(playerChar.damage);
        dialogue.text = "You attack! You deal " + playerChar.damage + "damage!";

        yield return new WaitForSeconds(2f);
        
        if (isdead)
        {
            Destroy(enemy);
            state = BattleState.Win;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(ETurn());
        }
    }

    IEnumerator PlayerSpecial()
    {
        dialogue.text = "You used your Divine Powers to heal";
        if (playerChar.maxHp - playerChar.currHp <= 15)
        {
            playerChar.currHp = playerChar.maxHp;
            playerChar.currMana -= 5;
        }
        else
        {
            playerChar.currHp += 15;
            playerChar.currMana -= 5;
        }
        playerUI.SetHp(playerChar.currHp);
        playerUI.SetMana(playerChar.currMana);
        yield return new WaitForSeconds(2f);
        
        state = BattleState.EnemyTurn;
        StartCoroutine(ETurn());
    }

    IEnumerator PlayerBlock()
    {
        // Block -> Need to find a way to nullify the enemy next attack
        yield return new WaitForSeconds(2f);
        // Change State
    }

    public void Attack()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void Special()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerSpecial());
    }

    public void Block()
    {
        if (state != BattleState.PlayerTurn)
            return;

        StartCoroutine(PlayerBlock());
    }

    IEnumerator ETurn()
    {
        dialogue.text = enemyChar.charName + "Attacks!";

        yield return new WaitForSeconds(2f);

        bool isdead = playerChar.TakeDamage(enemyChar.damage);
        dialogue.text = "You receive " + (enemyChar.damage-playerChar.def) + "damage!";
        playerUI.SetHp(playerChar.currHp);

        yield return new WaitForSeconds(1f);

        if (isdead)
        {
            Destroy(player);
            state = BattleState.Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            YourTurn();
        }
    }

    public void EndBattle()
    {
        if (state == BattleState.Win)
        {
            dialogue.text = "You defeated your enemy!";
        }
        else if(state == BattleState.Lost)
        {
            dialogue.text = "You Died";
        }
    }

}
