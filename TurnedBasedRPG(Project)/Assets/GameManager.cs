using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// put this script on any gameobject, i personally use an empty 'root' gameobject on which i put all my scripts alike
public class GameManager : MonoBehaviour
{
    public GameObject InventoryP;
    public bool Inventoryshown;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            Inventoryshown = !Inventoryshown;
        if(Inventoryshown == true)
        {
            InventoryP.SetActive(true);
        }else
        {
            InventoryP.SetActive(false);
        }
        
         
    }
}