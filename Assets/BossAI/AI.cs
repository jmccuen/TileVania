using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BossAI;
public class AI : MonoBehaviour
{
    public float difficultyFactor;
    public bool hasSpecial;
 
    public StateMachine<AI> stateMachine { get; set; }
 
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(Idle.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    void OnCollisionEnter(Collision collision){
           Debug.Log("AI:Player or something entered the box");
          // _owner.ChangeState(Attack.Instance);
        }
}
