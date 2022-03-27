using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xaxxroz : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          Debug.Log("Xaxxroz Class:Loaded, don't you feel special?");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
           Debug.Log("Xaxxroz Class:Player or something entered the box");
          // _owner.ChangeState(Attack.Instance);
    }

}
