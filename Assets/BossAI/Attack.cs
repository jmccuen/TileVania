using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossAI;

//create a random nubmer measure that against difficulty setting. If the number is less than transition directly to special attack
public class Attack : State<AI>
{
    private static Attack _instance;
    private Attack(){
        if(_instance != null) { return; }
        _instance = this;
    }

    public static Attack Instance{
        get{
            if(_instance==null){
                new Attack();
            }
            
            return _instance;
        }
    }
    public override void EnterState(AI _owner)
    {
        double difficultyFactor = 0.5; //This value should be linked up to the component config in the Uinity UI
        double dice = Random.value;
        if(dice < difficultyFactor){ //Also check out the hasSpecialAttack boolean here.
            //advance to state Special Attack
            }
        Debug.Log("Attack State [Enter]");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Attack State [Exit]");
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void UpdateState(AI _owner)
    {
        Debug.Log("Attack State [Update]");
    }
}
