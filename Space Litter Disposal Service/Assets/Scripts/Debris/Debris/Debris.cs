using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, ICollectable
{
    SpriteRenderer spriteRend;
    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;

    [SerializeField] int score;
    [SerializeField] int fuel;
    [SerializeField, Min(0.5f)] float size;

    public int Score => score;
    public int Fuel => fuel;

    Transform suckInSpot;

    public void Collect(Transform target)
    {
        //Play Noise
        //Play Particle Effect
        circleCollider2D.enabled = false;
        suckInSpot = target;
        InvokeRepeating("SuckIn", 0f, 0.05f);
    }

    public void SetData() {
        spriteRend = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        transform.localScale = Vector3.one * size;
        score = (int)((transform.localScale.magnitude) * 5f);
    }

    float lerp = 0;

    void SuckIn() {
        transform.localScale = Vector3.one * Mathf.Lerp(1, 0, lerp);
        transform.position = Vector3.Lerp(transform.position, suckInSpot.position, lerp);
        if (lerp >= 1) {
            Destroy(gameObject);
        }
        lerp += 0.05f;
    }
}
