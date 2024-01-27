using UnityEngine;


namespace Game
{
    public class SoundPlayer
    {
        private readonly EntitySound _sound;
        private EntitySoundFactory _soundFactory;
        private Pool<EntitySound> _pool;
        private PauseManager _pauseManager;

        public SoundPlayer(EntitySound sound, PauseManager pauseManager)
        {
            _sound = sound;
            _pauseManager = pauseManager;
        }

        public void Init()
        {
            _soundFactory = new(_sound);
            _pool = new(_soundFactory);
        }

        public void Play(Vector2 position)
        {
            var sound = _pool.Get(_sound);

            _pauseManager.Add(sound);

            sound.OnAudioEnded(explosion =>
            {
                Return(explosion);
                _pauseManager.Remove(explosion);
            });

            sound.transform.position = position;
            sound.Init();
        }

        public void Return(EntitySound sound)
        {
            _pool.Return(sound);
        }
    }
}