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
        //GameObject player = null;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 teste = (transform.position - player.transform.position).normalized;
            player.GetComponent<PlayerBehaviours>().SetDir_Box(new Vector3(Mathf.Round(teste.x), Mathf.Round(teste.y), Mathf.Round(teste.z)));
            Debug.Log((Mathf.Round(teste.x) + " / " + Mathf.Round(teste.y) + " / " + Mathf.Round(teste.z)));
            transform.parent = player.transform;
        }
        else
        {
            transform.parent = null;
            player = null;
        }
            
            
    }

}
