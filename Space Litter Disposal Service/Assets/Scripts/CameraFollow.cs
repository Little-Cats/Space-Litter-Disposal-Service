using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region fields
    public Transform targetTransform;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 followOffset;
    #endregion

    #region monobehaviour
    private void FixedUpdate()
    {
        Vector3 desiredPosition = targetTransform.position + followOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    #endregion

    #region methods

    #endregion
}
