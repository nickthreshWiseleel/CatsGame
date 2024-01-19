using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : IPausable
{
    public VFXPlayer()
    {
    }

    public IEnumerator Animate(Entity entity, string property, bool value)
    {
        entity.Animator.SetBool(property, value);

        yield return new WaitUntil(() => entity.IsAnimationEnded == true);
        Debug.Log("Animation is ended");
    }


    public void Pause()
    {
        Debug.Log("Paused");
    }

    public void Unpause()
    {
        Debug.Log("Unpaused");
    }
}