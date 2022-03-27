using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BossAI;

public class SpecialAttack : State<AI>
{
    private static SpecialAttack _instance;
    private SpecialAttack(){
        if(_instance != null) { return; }
        _instance = this;
    }

    public static SpecialAttack Instance{
        get{
            if(_instance==null){
                new SpecialAttack();
            }
            
            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        Debug.Log("SpecialAttack State [Enter]");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("SpecialAttack State [Exit]");
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void UpdateState(AI _owner)
    {
        Debug.Log("SpecialAttack State [Update]");
    }
}
