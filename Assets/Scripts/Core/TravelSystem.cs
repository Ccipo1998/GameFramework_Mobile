using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelSystem : Singleton<TravelSystem>, ISystem
{
    public delegate void TravelCompleteDelegate();
    public TravelCompleteDelegate _OnTravelComplete;

    [SerializeField]
    private string _LoadingSceneName;

    private string _currentSceneName;
    private string _targetSceneName;

    // flag -> loading coroutine running
    private bool _isLoadingDone;

    [SerializeField]
    private int _Priority;

    public int Priority => _Priority;

    public void Setup()
    {
        // initializations
        _isLoadingDone = true;
        _currentSceneName = SceneManager.GetActiveScene().name;

        // notify systems coordinator
        SystemsCoordinator.Instance.SystemReady(this);
    }

    public void SceneLoad(string scenePath) {
        // the loading has been called
        _isLoadingDone = false;

        StartCoroutine(Load(scenePath));
    }

    public bool IsLoadingDone()
    {
        return _isLoadingDone;
    }

    private IEnumerator Load(string scenePath) {
        _targetSceneName = scenePath;

        AsyncOperation loading_load = SceneManager.LoadSceneAsync(_LoadingSceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return loading_load.isDone; });

        AsyncOperation current = SceneManager.UnloadSceneAsync(_currentSceneName);
        yield return new WaitUntil(() => { return current.isDone; });

        AsyncOperation target = SceneManager.LoadSceneAsync(_targetSceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => { return target.isDone; });

        _currentSceneName = _targetSceneName;
        _targetSceneName = string.Empty;

        AsyncOperation loading_unload = SceneManager.UnloadSceneAsync(_LoadingSceneName);
        yield return new WaitUntil(() => { return loading_unload.isDone; });

        // the loading is done
        _isLoadingDone = true;

        _OnTravelComplete?.Invoke();
    }
}
