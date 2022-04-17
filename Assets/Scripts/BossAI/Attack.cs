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
        
        //correcting the original rotation
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


        /* Vector3 targetHeading = _owner.target.position - _owner.transform.position;
         Vector3 targetDirection = targetHeading.normalized;

         //rotate to look at the player

         _owner.transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
         _owner.transform.eulerAngles = new Vector3(0, _owner.transform.eulerAngles.y, 0);

         //move towards the player
         _owner.transform.position += _owner.transform.forward * _owner.speed * Time.deltaTime;
         // Debug.Log("Attack State [Update]");*/
        _owner.transform.LookAt(_owner.target.position);
        _owner.transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
        _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, _owner.target.transform.position, _owner.speed * Time.deltaTime);
    }
}
