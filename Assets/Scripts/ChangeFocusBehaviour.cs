using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeFocusBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject cam;
    [SerializeField] private Transform focus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam.GetComponent<CinemachineVirtualCamera>().LookAt = focus;
        }
    }
}
