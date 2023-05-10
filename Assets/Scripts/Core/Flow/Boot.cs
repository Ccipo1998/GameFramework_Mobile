using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// this class represents the Boot Unit inside the GameFlow FSM
public class Boot : Unit
{
    // Bolt syntax
    [DoNotSerialize]
    public ControlInput InputTrigger; // on enter event

    // Bolt syntax
    [DoNotSerialize]
    public ControlOutput OutputTrigger; // on exit event

    // definition of input and output events
    protected override void Definition()
    {
        InputTrigger = ControlInput(string.Empty, InternalBoot);
    }

    // template for input trigger
    private ControlOutput InternalBoot(Flow arg)
    {
        return OutputTrigger;
    }
}
