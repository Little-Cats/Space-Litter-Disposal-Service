using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Spawner/Debris")]
public class DebrisData : ScriptableObject
{
    public string name;
    public int score;
    public Sprite sprite;

    public DebrisData chunk;
    public int numChunks;

    public int health;
}