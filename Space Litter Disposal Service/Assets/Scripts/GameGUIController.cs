using TMPro;
using UnityEngine;

public class GameGUIController : MonoBehaviour
{
    #region fields
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fuelText;
    #endregion
    
    #region methods
    public void UpdateScoreText(int currScore, int maxScore)
    {
        scoreText.text = "Score: " + currScore + "/" + maxScore;
    }

    public void UpdateFuelText(int currFuel)
    {
        fuelText.text = "Fuel: " + currFuel;
    }
    #endregion

}
