using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    const string DEBRIS_TAG = "Debris";
    const float DESTROY_TIME = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("HIT");
        if (collision.gameObject.CompareTag(DEBRIS_TAG))
        {
            CircleCollider2D collider2D = collision.gameObject.GetComponent<CircleCollider2D>();

            collider2D.enabled = false;

            Destroy(collision.gameObject, DESTROY_TIME);

        }
    }
}
