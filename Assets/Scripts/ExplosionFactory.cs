using UnityEngine;

public class ExplosionFactory : IFactory<Explosion>
{
    private Explosion _explosion;

    public ExplosionFactory(Explosion explosion)
    {
        _explosion = explosion;
    }

    public Explosion Create()
    {
        var created = Object.Instantiate(_explosion);
        created.name = _explosion.gameObject.name;
        return created;
    }
}
