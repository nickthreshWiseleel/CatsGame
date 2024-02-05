using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public List<AudioClip> Audios;
    }
}