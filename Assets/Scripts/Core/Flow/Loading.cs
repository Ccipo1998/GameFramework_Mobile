using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Loading : Unit
{
    // Bolt syntax
    [DoNotSerialize]
    public ControlInput InputTrigger; // on enter event

    // Bolt syntax
    [DoNotSerialize]
    public ControlOutput OutputTrigger; // on exit event

    // Bolt syntax
    [DoNotSerialize]
    public ValueInput SceneToLoad;

    protected override void Definition()
    {
        InputTrigger = ControlInput(string.Empty, OnEnterLoading);
        OutputTrigger = ControlOutput(string.Empty);
        SceneToLoad = ValueInput<string>("Scene to load", string.Empty);
    }

    private ControlOutput OnEnterLoading(Flow arg)
    {
        // launch loading
        TravelSystem.Instance.SceneLoad(arg.GetValue<string>(SceneToLoad));

        return OutputTrigger;
    }
}
