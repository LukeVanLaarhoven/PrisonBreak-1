using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    private Inventory inventory;
    public float initialMaxWeight=100;
    
    void Start()
    {
        inventory = new Inventory(initialMaxWeight);
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 2))
            {
                IInteractable i = hit.collider.gameObject.GetComponent<IInteractable>();
                if (i != null)
                {
                    i.Action(this);
                }
            }
        }
    }
    
    public bool AddItem(Item i)
    {
        return inventory.AddItem(i);
    }

    public bool CanOpenDoor(int id)
    {
        return inventory.CanOpenDoor(id);
    }
     
}
