using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : Managers
{
    public Bounds bounds;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.localPosition + transform.parent.position, bounds.size);
    }
}
