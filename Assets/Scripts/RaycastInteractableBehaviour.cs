using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractableBehaviour : MonoBehaviour
{

    private HUDBehaviour scriptHud;
    private PlayerBehaviours scriptPlayer;
    void Start()
    {
        scriptHud = GameObject.Find("HUD").GetComponent<HUDBehaviour>();
        scriptPlayer = GetComponent<PlayerBehaviours>();

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (scriptHud.CheckCurrentInteractable(interactable.objectTag))
                {
                    scriptHud.UpdateInteractable(interactable.objectTag);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    scriptPlayer.Interactable(interactable.gameObject);
                    interactable.Interact();
                }
            }

        }
        else
        {
            scriptHud.UpdateInteractable("");
        }
    }
}
