using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, ICollectable
{
    public void Collect(PlayerCharacter player)
    {
        player.GainHealth(40);
        Debug.Log("Health recovered!");
        ObjectPools.instance.ReturnToPool(gameObject);
    }
}
