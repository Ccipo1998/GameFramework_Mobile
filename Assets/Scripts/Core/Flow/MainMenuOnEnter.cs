using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuOnEnter : OnEnterUnit
{
    // Bolt syntax
    [DoNotSerialize]
    public ValueInput MainMenuPrefab;

    protected override void VariablesDefinition()
    {
        MainMenuPrefab = ValueInput<GameObject>("Main menu prefab", null);
    }

    protected override ControlOutput OnEnter(Flow arg)
    {
        if (MainMenuPrefab != null)
            GameObject.Instantiate(arg.GetValue<GameObject>(MainMenuPrefab));

        return OutputTrigger;
    }
}
