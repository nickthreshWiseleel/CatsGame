using UnityEngine;

public interface ICancelableDelay

{
    Coroutine Delay { get; set; }
}
