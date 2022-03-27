using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using BossAI;

public class Taunt : State<AI>
{
    private static Taunt _instance;
    private Taunt(){
        if(_instance != null) { return; }
        _instance = this;
    }

    public static Taunt Instance{
        get{
            if(_instance==null){
                new Taunt();
                }
            
            return _instance;
            }
    }
    public override void EnterState(AI _owner)
    {
        Debug.Log("Taunt State [Enter]");
    }

    public override void ExitState(AI _owner)
    {
        
        Debug.Log("Taunt State [Exit]");
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void UpdateState(AI _owner)
    {
        Debug.Log("Taunt State [Update]");
    }
}
