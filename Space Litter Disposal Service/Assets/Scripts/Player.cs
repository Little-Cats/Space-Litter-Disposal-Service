using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region fields
    [SerializeField]
    private int maxScore = 100;
    private int score;
    private bool gameOver = false;

    private PlayerInput playerInput;

    [SerializeField]
    private float fuelConsumptionRate = 5f; // e.g consume fuel every 5 seconds
    [SerializeField]
    private int fuelTankCapacity = 100;
    private FuelTank fuelTank;
    private float elapsedTime;
    #endregion

    #region properties
    public int Score { get => score;}
    public bool GameOver { get => gameOver; }
    #endregion

    #region monobehaviour
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        fuelTank = new FuelTank(100, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null) //check for score
        {
            score += collectable.Score; //add score
            fuelTank.ChangeFuel(collectable.Fuel); //add fuel
        }
    }

    private void Update()
    {
        HandleFuelConsumption();
    }
    #endregion

    #region methods
    private void HandleFuelConsumption()
    {
        if (playerInput.MoveInput != Vector2.zero)
            elapsedTime += Time.deltaTime;
        if (elapsedTime >= fuelTank.ConsumeRate)
        {
            fuelTank.ConsumeFuel();
            elapsedTime = 0;
            gameOver = fuelTank.FuelIsEmpty();
        }
    }
    #endregion
}
