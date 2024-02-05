namespace Game
{
    public readonly struct Rewards
    {
        public readonly int Health, Score, Money, Destroyed;

        public Rewards(int health, int score, int money, int destroyed)
        {
            Health = health;
            Score = score;
            Money = money;
            Destroyed = destroyed;
        }
    }
}