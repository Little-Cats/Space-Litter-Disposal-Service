using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region fields
    // public GameGUIController guiController;
    public GameObject lowFuelWarning;
    public Slider fuelSlider;
    public Slider scoreSlider;
    

    [SerializeField]
    private int maxScore = 1000;
    private int score;
    private bool gameOver = false;
    private Rigidbody2D rb;
    private float startingDrag;
    private float lowFuelDrag;

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
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        lowFuelWarning.SetActive(false);
    }

    void Start()
    {
        fuelTank = new FuelTank(fuelTankCapacity, fuelConsumptionRate);
        SetMaxFuel(fuelTankCapacity);
        init_score();
        startingDrag = rb.drag;
        lowFuelDrag = startingDrag / 2;
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
            HandleDrag();
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

    private void HandleDrag()
    {
        float fuelRatio = (float)fuelTank.CurrentFuelAmount / (float)fuelTankCapacity;
        if (fuelRatio <= 0.35f && rb.drag != lowFuelDrag)
        {
            rb.drag = lowFuelDrag;
        }
        else if (fuelRatio > 0.35f && rb.drag != startingDrag)
        {
            rb.drag = startingDrag;
            lowFuelWarning.SetActive(false);
        }
    }
    #endregion

    #region gui
    private void HandleGUIUpdates()
    {
        float fuelRatio = (float)fuelTank.CurrentFuelAmount / (float)fuelTankCapacity;
        fuelSlider.value = fuelTank.CurrentFuelAmount;
        if (!lowFuelWarning.activeSelf && fuelRatio <= 0.35f)
        {
            lowFuelWarning.SetActive(true);
        }
        else if (lowFuelWarning.activeSelf && fuelRatio > 0.35f)
        {
            lowFuelWarning.SetActive(false);
        }
    }
    #endregion
}
