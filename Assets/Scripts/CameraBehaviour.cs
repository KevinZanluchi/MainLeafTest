using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    [Header("Position")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 cameraOffset;
    private float smoothFactor = 1f;

    void Start()
    {
        cameraOffset = transform.position - targetTransform.position;
    }


    private void LateUpdate()
    {
        Vector3 targetCamPos = targetTransform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothFactor * Time.deltaTime);
      
    }
}
