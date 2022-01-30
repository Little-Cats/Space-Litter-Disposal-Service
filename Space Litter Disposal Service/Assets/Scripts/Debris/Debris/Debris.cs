using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour, ICollectable
{
    SpriteRenderer spriteRend;
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider;

    [SerializeField] int score;
    [SerializeField] float size;

    public int Score => score;
    public int Fuel => 5;

    public void Collect()
    {
        //Play Noise
        //Play Particle Effect
    }

    private void OnDestroy()
    {
    }

    public void SetData(DebrisData _data) {
        spriteRend = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
}