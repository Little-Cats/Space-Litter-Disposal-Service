using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region fields
    // public GameGUIController guiController;
    public Slider fuelSlider;
    public Slider scoreSlider;
    

    [SerializeField]
    private int maxScore = 1000;
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

    [SerializeField] Transform suckInSpot;

    #region monobehaviour
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        fuelTank = new FuelTank(fuelTankCapacity, fuelConsumptionRate);
        SetMaxFuel(fuelTankCapacity);
        init_score();
        HandleGUIUpdates();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null) //check for score
        {
            score += collectable.Score; //add score
            fuelTank.ChangeFuel(collectable.Fuel); //add fuel
            HandleGUIUpdates();
        }
    }*/

    const string DEBRIS_TAG = "Debris";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(DEBRIS_TAG))
        {
            Debris debris = collision.gameObject.GetComponent<Debris>();
            debris.Collect(suckInSpot);
            score += debris.Score;
            fuelTank.ChangeFuel(debris.Fuel);

            //Update slider and Check for end game
            scoreSlider.value = score;
            if(score >= maxScore){
                score = 0;
                SceneManager.LoadScene(4);
            }
        }
    }
    private void init_score(){
        score = 0;
        scoreSlider.maxValue = maxScore;
        scoreSlider.value = score;
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
            if(fuelTank.FuelIsEmpty()){
                SceneManager.LoadScene(3);
            }
        }
    }
    public void SetMaxFuel(int fuelTankCapacity){
        fuelSlider.maxValue = fuelTankCapacity;
        fuelSlider.value = fuelTankCapacity;
    }
    #endregion

    #region gui
    private void HandleGUIUpdates()
    {
        fuelSlider.value = fuelTank.CurrentFuelAmount;
    }
    #endregion
}
