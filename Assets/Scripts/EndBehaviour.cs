using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBehaviour : MonoBehaviour
{
    private GameManeger scriptManeger;
    [SerializeField] private int nextFase;

    private void Start()
    {
        scriptManeger = GameObject.Find("GameManeger").GetComponent<GameManeger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            scriptManeger.EndFase(nextFase);
        }
    }
}
