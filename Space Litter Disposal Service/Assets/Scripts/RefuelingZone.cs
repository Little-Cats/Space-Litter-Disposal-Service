using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelingZone : MonoBehaviour
{
    #region fields
    [SerializeField]
    private float refuelRate = 0.5f;
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(Refuel(player));
        }
    }

    private IEnumerator Refuel(Player player) {
        player.Refuel();
        yield return new WaitForSeconds(refuelRate);
    }
}
