using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour :Interactable
{
    private bool interacting = false;
    public Rigidbody rig;
    private float speed = 0.5f;
    public GameObject player;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    public override void Interact()
    {
        base.Interact();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.parent = player.transform;
        }
        else
        {
            transform.parent = null;
            player = null;
        }
            
            
    }

}
