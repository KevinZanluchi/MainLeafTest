using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 6;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float originalStepOffset;

    [SerializeField] private Transform box;
    [SerializeField] private Transform hand;

    private HUDBehaviour scriptHud;
    enum StatusPlayer {Walk, DragBox };

    StatusPlayer currentStatus;

    // Start is called before the first frame update
    void Start()
    {
        scriptHud = GameObject.Find("HUD").GetComponent<HUDBehaviour>();
        SetStatusPlayer(StatusPlayer.Walk);
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (scriptHud.CheckCurrentInteractable())
                {
                    scriptHud.UpdateInteractable(interactable.objectTag);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (box == null)
                    {
                        SetStatusPlayer(StatusPlayer.DragBox);
                        box = hit.collider.transform;
                        transform.LookAt(box.position);
                        box.position = hand.position;
                        box.parent = gameObject.transform;
                        SetSpeed(0.5f);
                    }
                    else
                    {
                        SetStatusPlayer(StatusPlayer.Walk);
                        box.transform.parent = null;
                        SetSpeed(6);
                        box = null;
                    }
                }
            }

        }
        else
        {
            scriptHud.UpdateInteractable("");
        }

    }

    private void LateUpdate()
    {
        Move();
    }

    // movimentação e pulo
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(x, 0, z);

        
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        //movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        //Debug.Log(movementDirection);
        gravity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && GetStatusPlayer() == StatusPlayer.Walk)
        {
            controller.stepOffset = originalStepOffset;
            gravity = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpSpeed;
            }
        }
        else
        {
            controller.stepOffset = 0;
        }

        Vector3 move = movementDirection * magnitude;
        move.y = gravity;

        controller.Move(move * Time.deltaTime);


        if (movementDirection != Vector3.zero && GetStatusPlayer() == StatusPlayer.Walk)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        
    }

    void FaceTarget()
    {
        Vector3 direction = (box.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0f,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * 5f);
    }


    private void SetStatusPlayer(StatusPlayer status)
    {
        currentStatus = status;
    }

    private StatusPlayer GetStatusPlayer()
    {
        return currentStatus;
    }

    private void SetSpeed(float value)
    {
        speed = value;
    }
}
