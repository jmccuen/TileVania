using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilKnight : MonoBehaviour
{
    private AI ai;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
          Debug.Log("EvilKnight Class:Loaded, don't you feel special?");
          ai = GetComponent<AI>();
          animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            // Debug.Log("Xaxxroz Class:Player or something entered the box");
            ai.stateMachine.ChangeState(Attack.Instance);
            animator.SetBool("isRunning", true);
           // animator.Play("RedAttack");
            //StartCoroutine(RedAttack());
        }
    }

    IEnumerator RedAttack()
    {
        yield return new WaitForSeconds(5);
        ai.stateMachine.ChangeState(Idle.Instance);
    }



}
