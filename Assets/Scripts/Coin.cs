using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void Collect(PlayerCharacter player)
    {
        Debug.Log("Coin Picked!");
        ObjectPools.instance.ReturnToPool(gameObject);
        GameManager.instance.AddCoin(1);
    }
}
