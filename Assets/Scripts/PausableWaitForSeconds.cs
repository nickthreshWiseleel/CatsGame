using System.Collections;
using UnityEngine;

class PauseToken
{
    public bool IsPaused { get; private set; }
}
class PausableWaitForSeconds : CustomYieldInstruction
{
    private float _seconds;
    private PauseToken _token;

    public PausableWaitForSeconds(float seconds, MonoBehaviour mono, PauseToken token)
    {
        _seconds = seconds;
        _token = token;
        mono.StartCoroutine(TimeDecreaser());
    }

    private IEnumerator TimeDecreaser()
    {
        while (_seconds >= 0)
        {
            if (!_token.IsPaused)
            {
                _seconds -= Time.deltaTime;
                yield return null;
            }
        }
    }

    public override bool keepWaiting
    {
        get
        {
            if (_token.IsPaused) return true;

            if (_seconds < 0) return false;

            return true;
        }
    }
}