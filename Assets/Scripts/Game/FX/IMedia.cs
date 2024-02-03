using System;
using UnityEngine;

public interface IMedia<T> where T : Component
{
    T OnEffectEnded(Action<T> ended);
    void Play();
}
