using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region fields
    // public GameGUIController guiController;
    public Slider slider;

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
    public bool GameOver { get => gameOver; }
    public FuelTank FuelTank { get => fuelTank; }
    #endregion

    #region monobehaviour
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        fuelTank = new FuelTank(fuelTankCapacity, fuelConsumptionRate);
        SetMaxFuel(fuelTankCapacity);
        score = 0;
        HandleGUIUpdates();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null) //check for score
        {
            score += collectable.Score; //add score
            fuelTank.ChangeFuel(collectable.Fuel); //add fuel
            HandleGUIUpdates();
        }
    }

    private void Update()
    {
        HandleFuelConsumption();
    }
    #endregion

    #region methods
    public void Refuel()
    {
        fuelTank.Refuel();
        HandleGUIUpdates();
    }

    private void HandleFuelConsumption()
    {
        if (playerInput.MoveInput != Vector2.zero)
            elapsedTime += Time.deltaTime;
        if (elapsedTime >= fuelTank.ConsumeRate)
        {
            fuelTank.ConsumeFuel();
            HandleGUIUpdates();
            elapsedTime = 0;
            gameOver = fuelTank.FuelIsEmpty();
        }
    }
    public void SetMaxFuel(int fuelTankCapacity){
        slider.maxValue = fuelTankCapacity;
        slider.value = fuelTankCapacity;
    }
    #endregion

    #region gui
    private void HandleGUIUpdates()
    {
        // guiController.UpdateScoreText(score, maxScore);
        // guiController.UpdateFuelText(fuelTank.CurrentFuelAmount);
        slider.value = fuelTank.CurrentFuelAmount;
    }
    #endregion
}
