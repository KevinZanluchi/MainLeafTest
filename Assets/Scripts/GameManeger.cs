using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    private int score = 0;
    public HUDBehaviour scriptHud;
    public static GameManeger instance;
    private void OnLevelWasLoaded(int level)
    {

        if (instance == null)
        { //Se a variável estática estiver nula,
            instance = this; //Atribui o OBJETO GameManager a variável estática.
            DontDestroyOnLoad(gameObject);//Define que o objeto não deve ser destruído
        }
        else if (instance != this) //Se a variável for diferente de this, já foi criada antes
            Destroy(gameObject);//Destrói o objeto  
        instance.StarGame();
    }
    private void Awake()
    {
        if (instance == null)
        { //Se a variável estática estiver nula,
            instance = this; //Atribui o OBJETO GameManager a variável estática.
            DontDestroyOnLoad(gameObject);//Define que o objeto não deve ser destruído
        }
        else if (instance != this) //Se a variável for diferente de this, já foi criada antes
            Destroy(gameObject);//Destrói o objeto  
        instance.StarGame();
    }

    private void StarGame()
    {
        scriptHud = GameObject.Find("HUD").GetComponent<HUDBehaviour>();
    }

    public void Score(int scoreCoin)
    {
        score += scoreCoin;
        scriptHud.updateScore(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void EndFase(int fase)
    {
        SceneManager.LoadScene(fase);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
