using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : Singleton<UISystem>, ISystem
{
    [SerializeField]
    private int _Priority;
    public int Priority => _Priority;

    [SerializeField]
    private IdContainerGameEvent _ViewChangedStateEvent;
    public IdContainerGameEvent ViewChangedStateEvent => _ViewChangedStateEvent;

    // to store all the already instatiated UIs (id <-> view)
    private Dictionary<string, ViewController> _viewControllerDictionary;

    // where to spawn the new views
    [SerializeField]
    private GameObject _SpawnPoint;

    public void Setup()
    {
        _viewControllerDictionary = new Dictionary<string, ViewController>();
        SystemsCoordinator.Instance.SystemReady(this);
    }

    public ViewController ShowView(idContainer id, ViewController controller)
    {
        if (_viewControllerDictionary.ContainsKey(id.Id))
        {
            Debug.LogError("Tentativo di instanziamento di una View già istanziata con id: " + id.Id);
            return null;
        }

        ViewController newController = Instantiate(controller, _SpawnPoint.transform);
        _viewControllerDictionary.Add(id.Id, newController);

        newController.Setup(id);
        newController.State = ViewController.ViewState.Showing;

        return newController;
    }

    public void HideView(idContainer id)
    {
        if (!_viewControllerDictionary.ContainsKey(id.Id))
        {
            Debug.LogError("Tentativo di distruzione di una View non istanziata con id: " + id.Id);
            return;
        }

        ViewController view = _viewControllerDictionary[id.Id];
        view.State = ViewController.ViewState.Hiding;
        StartCoroutine(WaitUntilViewHidden(view));
    }

    private IEnumerator WaitUntilViewHidden(ViewController controller)
    {
        yield return new WaitUntil(() => { return controller.State == ViewController.ViewState.Hidden; });
        _viewControllerDictionary.Remove(controller.IdContainer.Id);
        Destroy(controller.gameObject);
    }
}
