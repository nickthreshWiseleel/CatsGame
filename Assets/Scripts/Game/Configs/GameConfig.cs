using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DataConfig", menuName = "Configs/DataConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public int StartHealth = 155;
        public int StartScore = 0;
        public int StartMoney = 0;
        public int StartDestroyed = 0;
        public int HealthDecrement = -1;
        public int ScoreIncrement = 1;
        public int MoneyIncrement = 2;
        public int DestroyedIncrement = 1;
    }
}