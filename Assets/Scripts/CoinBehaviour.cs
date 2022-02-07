using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public CoinObject coin;
    [SerializeField] private GameManeger scriptManeger;
    [SerializeField] private int score;
    [SerializeField] private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        scriptManeger = GameObject.Find("GameManeger").GetComponent<GameManeger>();
        score = coin.score;
        rend.material.SetTexture("_MainTex", coin.texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            scriptManeger.Score(score);
            Destroy(this.gameObject);
        }
    }
}
