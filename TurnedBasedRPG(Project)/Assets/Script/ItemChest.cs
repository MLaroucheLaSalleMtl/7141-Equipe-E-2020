using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;


    private bool isInRange;
    public void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {
            if (item != null)
            {
                inventory.AddItem(item);
                item = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
}
