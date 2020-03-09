using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public Slider hpSlider;
    public Slider manaSlider;

    public void SetUI(Fighter player)
    {
        hpSlider.maxValue = player.maxHp;
        hpSlider.value = player.currHp;
        manaSlider.maxValue = player.maxMana;
        manaSlider.value = player.currMana;
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMana(int mana)
    {
        manaSlider.value = mana;
    }
}
