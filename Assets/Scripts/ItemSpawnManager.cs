using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject PickUpItemPreFabs;

    public void SpawnItem(Vector3 position, Items item, int count)
    {
        GameObject o = Instantiate(PickUpItemPreFabs, position, Quaternion.identity);
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}
