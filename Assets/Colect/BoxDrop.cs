using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrop : PropDestructible
{
    public int MonedasMax;
    public int GemasMax;

    public float RangeItems;

    public GameObject Gems;
    public GameObject Coins;

    public override void Die()
    {
        int cantidad = Random.Range(1, GemasMax + 1);

        for (int i = 0; i < cantidad; i++)
        {
            GameObject GemsClone = Instantiate(Gems,transform.position + new Vector3(Random.Range(-RangeItems, RangeItems),0, Random.Range(-RangeItems, RangeItems)) ,Quaternion.identity);
        }

        cantidad = Random.Range(0, MonedasMax + 1);

        for (int i = 0; i < cantidad; i++)
        {
            GameObject GemsClone = Instantiate(Coins, transform.position + new Vector3(Random.Range(-RangeItems, RangeItems), 0, Random.Range(-RangeItems, RangeItems)), Quaternion.identity);
        }

        base.Die();
    }
}
