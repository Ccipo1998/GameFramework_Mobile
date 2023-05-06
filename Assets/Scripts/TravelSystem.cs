using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelSystem : Singleton<TravelSystem>
{
    public delegate void TravelCompleteDelegate();
    public TravelCompleteDelegate _OnTravelComplete;

    [SerializeField]
    private string _InitialScenePath;

    [SerializeField]
    private string _LoadingScenePath;

    private string _currentScenePath;
    private string _targerScenePath;

    private void Start() {
        _currentScenePath = SceneManager.GetActiveScene().name;
        SceneLoad(_InitialScenePath);
    }

    public void SceneLoad(string scenePath) {
        StartCoroutine(Load(scenePath));
    }

    private IEnumerator Load(string scenePath) {
        _targerScenePath = scenePath;

        AsyncOperation loading_load = SceneManager.LoadSceneAsync(_LoadingScenePath, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return loading_load.isDone; });

        AsyncOperation current = SceneManager.UnloadSceneAsync(_currentScenePath);
        yield return new WaitUntil(() => { return current.isDone; });

        AsyncOperation target = SceneManager.LoadSceneAsync(_targerScenePath, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return target.isDone; });

        _currentScenePath = _targerScenePath;
        _targerScenePath = string.Empty;

        AsyncOperation loading_unload = SceneManager.UnloadSceneAsync(_LoadingScenePath);
        yield return new WaitUntil(() => { return loading_unload.isDone; });

        _OnTravelComplete?.Invoke();
    }
}
