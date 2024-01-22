using UnityEngine;

public class PausableWaitForSeconds : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetMouseButtonDown(1);
        }
    }

    public PausableWaitForSeconds()
    {
        Debug.Log("Waiting for Mouse right button down");
    }
}
