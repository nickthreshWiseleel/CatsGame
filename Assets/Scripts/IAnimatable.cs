using UnityEngine;

public interface IAnimatable
{
    Animator Animator { get; }
    bool IsAnimationEnded { get; }
}
