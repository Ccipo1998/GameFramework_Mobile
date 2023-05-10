using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseViewController : MonoBehaviour
{
    [SerializeField]
    private idContainer _ResumeIdProvider;

    public void Resume()
    {
        PlayerController.Instance.EnableInputProvider(_ResumeIdProvider.Id);
        Destroy(gameObject);
    }

    public void ChangeScene(string scene)
    {
        TravelSystem.Instance.SceneLoad(scene);
    }
}
