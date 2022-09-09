using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine;

public abstract class Entity
{
    public bool IsDestroyed { get; private set; }
    public BaseGame Game { get; private set; }
    protected Entity(BaseGame game)
    {
        IsDestroyed = false;
        Game = game;
    }

    public abstract void Update(float dt);
    public abstract void Draw(SpriteBatch sb);

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }
}