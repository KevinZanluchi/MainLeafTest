using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour
{
    private GameManeger scriptManeger;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text interactableText;

    // Start is called before the first frame update
    void Start()
    {
        scriptManeger = GameObject.Find("GameManeger").GetComponent<GameManeger>();
        updateScore(scriptManeger.GetScore());
    }

    public void UpdateInteractable( string newInteractable)
    {
        interactableText.text = newInteractable;
    }

    public bool CheckCurrentInteractable()
    {
        if (interactableText.text == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
