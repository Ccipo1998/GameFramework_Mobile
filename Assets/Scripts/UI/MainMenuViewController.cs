using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField]
    private OptionViewController _OptionViewPrefab;

    private OptionViewController _optionViewController;

    [SerializeField]
    private string _OnStartGameFlowEvent;

    public void StartGame(string sceneName)
    {
        //TravelSystem.Instance.SceneLoad(scene);

        BoltFlowSystem.Instance.SetFSMvariable("SCENE_TO_LOAD", sceneName);
        BoltFlowSystem.Instance.TriggerFSMevent(_OnStartGameFlowEvent);
    }

    public void OpenOptions()
    {
        if (_optionViewController) // == null
            return;

        _optionViewController = Instantiate(_OptionViewPrefab);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
