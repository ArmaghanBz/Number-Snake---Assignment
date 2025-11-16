using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int removeCount = 1; // how many numbers to remove from head
    public float slowSeconds = 0.8f;

    public void Hit(PlayerHead head)
    {
        //head.RemoveTailCount(removeCount); Multiple approaches can be applied as for now slowing down the speed is applied
        StartCoroutine(ApplySlow(head));
        
    }

    private IEnumerator ApplySlow(PlayerHead head)
    {
        float original = head.forwardSpeed;
        head.forwardSpeed = Mathf.Max(1f, original * 0.5f);
        yield return new WaitForSeconds(slowSeconds);
        head.forwardSpeed = original;
    }
}
