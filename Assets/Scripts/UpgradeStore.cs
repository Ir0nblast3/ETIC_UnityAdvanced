using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStore : MonoBehaviour, IInteractable
{
    private PlayerCharacter _player;

    public void Interact(PlayerCharacter player)
    {
        GameManager.instance.OpenStore();
        _player = player;
        Debug.Log("Store Open");
    }

    public void TripleJumpUpgrade()
    {
        if (GameManager.instance.PlayerCoinNumber >= 20)
        {
            GameManager.instance.RemoveCoin(20);
            _player.MaxJumps = 2;
            Debug.Log("Upgrade bought");
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }

    public void DoubleAmmoUpgrade()
    {
        if (GameManager.instance.PlayerCoinNumber >= 40)
        {
            GameManager.instance.RemoveCoin(40);
            Debug.Log("Upgrade bought");
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }

    public void DoubleDamageUpgrade()
    {
        if (GameManager.instance.PlayerCoinNumber >= 40)
        {
            GameManager.instance.RemoveCoin(40);
            _player.EquippedGun.GunDamage *= 2;
            Debug.Log("Upgrade bought");
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }

    public void HealthPackSpawnRate()
    {
        if (GameManager.instance.PlayerCoinNumber >= 30)
        {
            GameManager.instance.RemoveCoin(30);
            Debug.Log("Upgrade bought");
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }
}
