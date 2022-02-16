using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider sliderLoading;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsychronously(sceneIndex));
    }

    private IEnumerator LoadAsychronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            sliderLoading.value = progress;

            yield return null;
        }
    }
}
