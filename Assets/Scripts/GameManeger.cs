using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private HUDBehaviour scriptHud;
    [SerializeField] private LevelLoaderBehaviour scriptLevelLoader;

    [SerializeField] private List<Transform> boxs;
    public List<float> box1;
    public List<float> box2;
    private bool gameOver = false;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            boxs = GameObject.Find("Reference").GetComponent<SaveReferenceScene>().GetBoxs();
            if (gameOver)
            {
                Load();
                gameOver = false;
            }
        }
        scriptHud = GameObject.Find("HUD").GetComponent<HUDBehaviour>();
        scriptLevelLoader = GameObject.Find("HUD").GetComponent<LevelLoaderBehaviour>();
    }

    private void Save()
    {
        GetBoxsTransforms(box1,boxs[0]);
        GetBoxsTransforms(box2, boxs[1]);
        Save_Load.Save(this);
    }

    private void Load()
    {
        SaveConfig data = Save_Load.Load();

        if (data != null)
        {
            SetBoxPosition(data.box1, boxs[0]);
            SetBoxPosition(data.box2, boxs[1]);
        }
        else
        {
            Debug.Log("Error");
        }
    }

    private void GetBoxsTransforms(List<float> box, Transform boxPosition)
    {
        box[0] = boxPosition.position.x;
        box[1] = boxPosition.position.y;
        box[2] = boxPosition.position.z;

    }

    private void SetBoxPosition(List<float> box, Transform boxPosition)
    {
        boxPosition.position = new Vector3(box[0],box[1],box[2]);
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
        if (SceneManager.GetActiveScene().buildIndex  == 0)
        {
            Save();
        }
        scriptLevelLoader.LoadLevel(fase);
        //SceneManager.LoadScene(fase);
    }

    public void GameOver()
    {
        gameOver = true;
        Score(-score);
        scriptLevelLoader.LoadLevel(0);
        //SceneManager.LoadScene(0);
    }
}
