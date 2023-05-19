using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseOnEnter : OnEnterUnit
{
    // Bolt syntax
    [DoNotSerialize]
    public ValueInput PauseMenuPrefab;

    protected override void VariablesDefinition()
    {
        PauseMenuPrefab = ValueInput<GameObject>("Pause menu prefab", null);
    }

    protected override ControlOutput OnEnter(Flow arg)
    {
        if (PauseMenuPrefab != null)
            GameObject.Instantiate(arg.GetValue<GameObject>(PauseMenuPrefab));

        return OutputTrigger;
    }
}
