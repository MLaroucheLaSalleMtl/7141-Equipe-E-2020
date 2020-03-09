using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipementPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipementSlot[] equipementSlots;


    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < equipementSlots.Length; i++)
        {
            equipementSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipementSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipementSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipementSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipementSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipementSlots[i].OnDragEvent += OnDragEvent;
            equipementSlots[i].OnDropEvent += OnDropEvent;

        }
    }

    private void OnValidate()
    {
        equipementSlots = equipmentSlotsParent.GetComponentsInChildren<EquipementSlot>();
    }
    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipementSlots.Length; i++)
        {
            if (equipementSlots[i].EquipementType == item.EquipementType)
            {
                previousItem = (EquippableItem)equipementSlots[i].Item;
                equipementSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;

    }

    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i< equipementSlots.Length; i++)
        {
            if(equipementSlots[i].Item == item)
            {
                equipementSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}