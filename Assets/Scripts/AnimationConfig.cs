using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationConfig", menuName = "Configs/AnimationConfig", order = 1)]
public class AnimationConfig : ScriptableObject
{
    public List<Sprite> Sprites;
}
