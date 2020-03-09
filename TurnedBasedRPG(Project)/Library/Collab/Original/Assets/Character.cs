using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;
    public int charLvl;
    public int damage;
    public int def;
    public int maxHp;
    public int currHp;
    public int maxMana;
    public int currMana;

    public bool TakeDamage(int dmg)
    {
        currHp -= dmg - def;
        if (currHp <= 0)
            return true;
        else
            return false;        
    }

}
