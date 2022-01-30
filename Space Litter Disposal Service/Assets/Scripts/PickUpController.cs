using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControler : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    const string DEBRIS_TAG = "Debris";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("HIT");
        if (collision.gameObject.CompareTag(DEBRIS_TAG))
        {
            Debris debris = collision.gameObject.GetComponent<Debris>();
            debris.Collect(transform);
        }
    }
}
