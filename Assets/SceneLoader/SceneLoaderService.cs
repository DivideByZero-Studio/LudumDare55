using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderService : MonoBehaviour
{
    private bool isLoading;

    public void LoadScene(string sceneName)
    {
        if (isLoading) return;

        var currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == sceneName)
            throw new Exception("You are trying to load already loaded scene");

        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    public void LoadSceneInstantly(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        if (isLoading) return;

        StartCoroutine(LoadSceneRoutine(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isLoading = true;

        var waitFading = true;
        Fader.Instance.FadeIn(() => waitFading = false);
        while (waitFading) yield return null;

        var async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) yield return null;

        async.allowSceneActivation = true;

        waitFading = true;
        Fader.Instance.FadeOut(() => waitFading = false);
        while (waitFading) yield return null;

        isLoading = false;
    }
}