using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeFocusBehaviour : MonoBehaviour
{

    [SerializeField] private CameraBehaviour scriptCam;
    [SerializeField] private Transform areaFocus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scriptCam.SetAreaFocus(areaFocus);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scriptCam.RemoveAreaFocus();
        }
    }
}
