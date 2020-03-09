using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementSlot : ItemSlot
{
    public EquipementType EquipementType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipementType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.EquipementType == EquipementType;
    }
}
