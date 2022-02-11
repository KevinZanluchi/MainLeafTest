﻿using System.Collections;
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

    [SerializeField] private Animator anim;

    [SerializeField] private  Interactable box;

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

        switch (GetStatusPlayer())
        {
            case StatusPlayer.Walk:


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
                            SetStatusPlayer(StatusPlayer.DragBox);
                            box = hit.collider.gameObject.GetComponent<Interactable>();
                            SetSpeed(0.5f);
                            transform.LookAt(box.gameObject.transform.position);
                            box.Interact();

                        }
                    }

                }
                else
                {
                    scriptHud.UpdateInteractable("");
                }
                break;
            case StatusPlayer.DragBox:
                if (Input.GetMouseButtonDown(0))
                {
                    SetStatusPlayer(StatusPlayer.Walk);
                    box.Interact();
                    SetSpeed(3);
                }
                break;
        }


    }

    private void LateUpdate()
    {
        Move();
        Jump();
        Crunch();
    }

    // movimentação e pulo
    private void Move()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movementDirection = GetDirectionMove(x,z);

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        anim.SetFloat("Move", magnitude);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        gravity += Physics.gravity.y * Time.deltaTime;

        Vector3 move = movementDirection * magnitude;
        move.y = gravity;

        controller.Move(move * Time.deltaTime);


        if (movementDirection != Vector3.zero && GetStatusPlayer() == StatusPlayer.Walk)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        
    }

    private void Jump()
    {

        if (controller.isGrounded && GetStatusPlayer() == StatusPlayer.Walk)
        {
            anim.SetBool("Jump", false);
            controller.stepOffset = originalStepOffset;
            gravity = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpSpeed;
                anim.SetBool("Jump", true);
            }
        }
        else
        {
            controller.stepOffset = 0;
        }
    }

    private void Crunch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("Crunch", true);
            controller.center = new Vector3(0,0.4f,0);
            controller.height = 1;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("Crunch", false);
            controller.center = new Vector3(0, 0.8f, 0);
            controller.height = 1.6f;
        }
    }

    private Vector3 GetDirectionMove(float x, float z)
    {
        Vector3 direction;

        if (GetStatusPlayer() == StatusPlayer.DragBox)
        {
            direction = new Vector3(0, 0, z);
        }
        else
        {
            direction = new Vector3(x, 0, z);
        }
        return direction;
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
