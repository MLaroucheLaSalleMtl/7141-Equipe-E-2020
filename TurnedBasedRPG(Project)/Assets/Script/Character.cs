using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;
using UnityEngine.UI;

public class Character:MonoBehaviour
{

  public CharacterStat Strength ;
  public CharacterStat Stamina;
  public CharacterStat Speed;
  public CharacterStat resistence;
    


    [SerializeField] Inventory inventory;
    [SerializeField] EquipementPanel equipementPanel;
    [SerializeField] StatPanel StatPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Awake()
    {
        StatPanel.SetStats(Strength, Stamina, Speed, resistence);
        StatPanel.UpdateStatValues();

        // Setup events;
        //right clock
        inventory.OnRightClickEvent += Equip;
        equipementPanel.OnRightClickEvent += Unequip;
        //
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipementPanel.OnPointerEnterEvent += ShowTooltip;
        //
        inventory.OnPointerExitEvent += HideTooltip;
        equipementPanel.OnPointerExitEvent += HideTooltip;
        //
        inventory.OnBeginDragEvent += BeingDrag;
        equipementPanel.OnBeginDragEvent += BeingDrag;
        //
        inventory.OnEndDragEvent += EndDrag;
        equipementPanel.OnEndDragEvent += EndDrag;
        //
        inventory.OnDragEvent += Drag;
        equipementPanel.OnDragEvent += Drag;
        //
        inventory.OnDropEvent += Drop;
        equipementPanel.OnDropEvent += Drop;
    }

   private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideToolTip();
    }

    private void BeingDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if (draggedSlot is EquipementSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            if(dropItemSlot is EquipementSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }
            StatPanel.UpdateStatValues();

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }





    private void UnequipFromEquiPanel(Item item)
    {
        if(item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }
    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipementPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    StatPanel.UpdateStatValues();
                }
                item.Equip(this);
                StatPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() && equipementPanel.RemoveItem(item))
        {
            item.Unequip(this);
            StatPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
