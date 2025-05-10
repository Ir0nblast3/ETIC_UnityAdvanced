using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCharacter player)
    {
        player.EquippedGun.Bullets = player.EquippedGun.MaxBullets;
        Debug.Log("Ammo Refilled");
    }
}
