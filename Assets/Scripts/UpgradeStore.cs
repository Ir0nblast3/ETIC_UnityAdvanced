using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStore : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacter player)
    {
        GameManager.instance.OpenStore();
        Debug.Log("Store Open");
    }
}
