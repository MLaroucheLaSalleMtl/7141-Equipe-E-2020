using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public enum EquipementType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public int SpeedBonus;
    public int StrengthBonus;
    public int StaminaBonus;
    public int resistenceBonus;
    [Space]
    public float SpeedPercentBonus;
    public float StrengthPercentBonus;
    public float StaminaPercentBonus;
    public float resistencePercentBonus;
    [Space]
    public EquipementType EquipementType;

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (StaminaBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaBonus, StatModType.Flat, this));
        if (SpeedBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        if (resistenceBonus != 0)
            c.resistence.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));

        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (StaminaPercentBonus != 0)
            c.Stamina.AddModifier(new StatModifier(StaminaPercentBonus, StatModType.PercentMult, this));
        if (SpeedPercentBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        if (resistencePercentBonus != 0)
            c.resistence.AddModifier(new StatModifier(resistencePercentBonus, StatModType.PercentMult, this));



    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Stamina.RemoveAllModifiersFromSource(this);
        c.Speed.RemoveAllModifiersFromSource(this);
        c.resistence.RemoveAllModifiersFromSource(this);
    }
}
