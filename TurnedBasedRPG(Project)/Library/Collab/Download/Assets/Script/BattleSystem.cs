using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { PlayerTurn, EnemyTurn, Lost, Win }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public GameObject enemy;

    public Transform enemySpawn;
    public Transform playerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.PlayerTurn;
        Instantiate(player, playerSpawn);
        Instantiate(enemy, enemySpawn);
    }
    
}
