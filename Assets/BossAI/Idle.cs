using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BossAI;

public class Idle : State<AI>
{
    private static Idle _instance;

    private Idle(){
        if(_instance != null) { return; }
        _instance = this;
    }

    public static Idle Instance{
        get{
            if(_instance==null){
                new Idle();
            }
            
            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        Debug.log("Idle State [Enter]");
    }

    public override void ExitState(AI _owner)
    {
        Debug.log("Idle State [Exit]");
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void UpdateState(AI _owner)
    {
        Debug.log("Idle State [Update]");
    }
}
