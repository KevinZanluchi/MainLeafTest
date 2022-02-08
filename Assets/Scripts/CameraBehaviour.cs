using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{

    //[Header("Position")]
    //[SerializeField] private Transform targetTransform;
    //[SerializeField] private Vector3 cameraOffset;
    //private float smoothFactor = 1f;

    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;

    public float minZoom = -11;
    public float maxZoom = -15;
    public float zoomLimiter = -20;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        //cameraOffset = transform.position - targetTransform.position;
        cam = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        //Vector3 targetCamPos = targetTransform.position + cameraOffset;
        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothFactor * Time.deltaTime);

        Move();
        Zoom();
      
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom,minZoom,GetGreatestDistance()/zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }

    void Move()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    public void SetAreaFocus(Transform newArea)
    {
        targets.Add(newArea);
    }

    public void RemoveAreaFocus()
    {
        if (targets.Count != 1)
        {
            targets.RemoveAt(targets.Count - 1);
        }
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {

        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
