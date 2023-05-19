using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseViewController : MonoBehaviour
{
    [SerializeField]
    private idContainer _ResumeIdProvider;

    [SerializeField]
    private string _OnReturnToFlowEvent;
    [SerializeField]
    private string _OnResumeFlowEvent;

    public void Resume()
    {
        BoltFlowSystem.Instance.TriggerFSMevent(_OnResumeFlowEvent);
        PlayerController.Instance.EnableInputProvider(_ResumeIdProvider.Id);

        Time.timeScale = 1.0f;

        Destroy(gameObject);
    }

    public void ReturnTo(string sceneName)
    {
        //TravelSystem.Instance.SceneLoad(scene);

        BoltFlowSystem.Instance.SetFSMvariable("SCENE_TO_LOAD", sceneName);
        BoltFlowSystem.Instance.TriggerFSMevent(_OnReturnToFlowEvent);

        Time.timeScale = 1.0f;
    }
}
