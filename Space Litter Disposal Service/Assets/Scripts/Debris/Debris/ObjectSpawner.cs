using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner spawner;

    [SerializeField] int maxObjects = 30;
    [SerializeField] List<GameObject> debrisType;
    List<GameObject> debris;

    [SerializeField] GameObject parent;

    BoxCollider2D boxCollider;
    Transform cameraT;

    Vector2 bounds;

    [SerializeField] Vector2 minMaxMomentum = new Vector2(1,1);
    [SerializeField] float randomTorue = 1f;
    [SerializeField, Min(0.5f)] Vector2 randomScale;
    [SerializeField] float respawnTimer = 5f;
    private void Awake()
    {
        cameraT = Camera.main.transform;
        boxCollider = GetComponent<BoxCollider2D>();
        bounds = boxCollider.bounds.size;

        debris = new List<GameObject>();
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        InvokeRepeating("ReSpawn", 0f, respawnTimer);
    }

    GameObject tmp;

    GameObject SpawnObject() {
        int randomData = Random.Range(0, debrisType.Count);
        tmp = Instantiate(debrisType[randomData], parent.transform);
        tmp.transform.position = GetRandomLocation();
        tmp.transform.localScale = GetRandomScale();
        tmp.GetComponent<Rigidbody2D>().AddForce(GetRandomMomentum(), ForceMode2D.Impulse);
        tmp.GetComponent<Rigidbody2D>().AddTorque(GetRandomTorque(), ForceMode2D.Impulse);
        return tmp;
    }

    #region Spawning

    const float radius = 50f;
    Vector2 GetRandomLocation()
    {
        rndLocation.x = Random.Range(-bounds.x / 2, bounds.x / 2);
        rndLocation.y = Random.Range(-bounds.y / 2, bounds.y / 2);

        if (Vector3.Distance(rndLocation, cameraT.position) < radius) {
            rndLocation.x = cameraT.position.x + Mathf.Cos(Random.value) * radius;
            rndLocation.y = cameraT.position.y + Mathf.Cos(Random.value) * radius;
        }

        return rndLocation;
    }

    Vector2 rndLocation;
    Vector2 rndMomentum;
    float torque;
    float scale;
    
    Vector2 GetRandomMomentum() {
        rndMomentum.x = Random.Range(-minMaxMomentum.x, minMaxMomentum.x);
        rndMomentum.y = Random.Range(-minMaxMomentum.y, minMaxMomentum.y);

        return rndMomentum;
    }

    float GetRandomTorque()
    {
        torque = Random.Range(-randomTorue, randomTorue);

        return torque;
    }

    Vector3 GetRandomScale()
    {
        scale = Random.Range(randomScale.x, randomScale.y);

        return Vector3.one * scale;
    }

    #endregion

    void ReSpawn() {
        //print("RESPAWN " + (debris.Count < maxObjects));
        for (int i = 0; i < debris.Count; i++)
        {
            if (debris[i] == null)
            {
                debris.RemoveAt(i);
            }
        }

        if (debris.Count <= maxObjects)
        {
            for (int i = 0; i < maxObjects - debris.Count; i++)
            {
                debris.Add(SpawnObject());
            }
        }
    }
}
