using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameEvent loadingScreenInEvent;

    public List<string> scenes = new List<string>();

    public void LoadScene(int sceneID)
    {
        /*
        Debug.Log("issue");
        if (loadingScreenInEvent != null)
            loadingScreenInEvent.Raise();
        SceneManager.LoadScene(scenes[sceneID]);*/
        LoadSceneAsync(sceneID);
    }

    public void LoadSceneAsync(int sceneID)
    {
        StartCoroutine(Loading(sceneID));
    }

    IEnumerator Loading(int sceneID)
    {
        if (loadingScreenInEvent != null)
            loadingScreenInEvent.Raise();

        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadSceneAsync(scenes[sceneID]);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
