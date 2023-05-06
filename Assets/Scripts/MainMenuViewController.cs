using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField]
    private OptionViewController _OptionViewPrefab;

    private OptionViewController _optionViewController;

    public void ChangeScene(string scene)
    {
        TravelSystem.Instance.SceneLoad(scene);
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
