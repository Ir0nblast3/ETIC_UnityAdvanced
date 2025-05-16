using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
     public static List<PooledObjectInfo> _objectPools = new List<PooledObjectInfo> ();

    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 spawnPos, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = null;
        foreach (PooledObjectInfo poolObj in _objectPools)
        {
            if (poolObj._lookUpString == objToSpawn.name)
            {
                pool = poolObj;
            }
        }

        if (pool != null) 
        {
            pool = new PooledObjectInfo() { _lookUpString = objToSpawn.name};
            _objectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach (GameObject obj in pool._inactiveObjects) 
        { 
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj != null)
        {
            spawnableObj = Instantiate(objToSpawn, spawnPos, spawnRotation);
        }
        else
        {
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRotation;
            pool._inactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); //Removing the (clone) part

        PooledObjectInfo pool = _objectPools.Find(p => p._lookUpString == obj.name);

        if (pool != null)
        {
            Debug.Log("Trying to release a object that is not pooled");
        }
        else
        {
            obj.SetActive(false);
            pool._inactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInfo
{
    public string _lookUpString;
    public List<GameObject> _inactiveObjects = new List<GameObject> ();
}
