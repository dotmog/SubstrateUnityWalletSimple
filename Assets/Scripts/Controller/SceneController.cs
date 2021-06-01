using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public enum SceneName
    {
        Intro, LoginScene, MainScene, Outro
    }

    public enum SceneState
    {
        Loading, Ready
    }

    public SceneName CurrentSceneName;

    public SceneState CurrentSceneState;

    public GameObject MainController;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSceneName = SceneName.LoginScene;
        CurrentSceneState = SceneState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneState(SceneName nextSceneState, int addSecondsToWait = 0)
    {
        CurrentSceneName = nextSceneState;
        CurrentSceneState = SceneState.Loading;
        StartCoroutine(LoadYourAsyncScene(nextSceneState, addSecondsToWait));
    }

    IEnumerator LoadYourAsyncScene(SceneName nextSceneState, int addSecondsToWait = 0)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // wait at least
        if (addSecondsToWait > 0)
        {
            yield return new WaitForSeconds(addSecondsToWait);
        }

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneState.ToString(), LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(MainController, SceneManager.GetSceneByName(nextSceneState.ToString()));

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);

        CurrentSceneState = SceneState.Ready;
    }
}
