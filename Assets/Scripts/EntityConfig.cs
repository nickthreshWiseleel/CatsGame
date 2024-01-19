using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig", menuName = "Configs/EntityConfig", order = 1)]
public class EntityConfig : ScriptableObject
{
    public string Name;
    public Sprite StartSprite;
    public AnimationClip AnimationClip;
    public AudioClip AudioClip;
}
