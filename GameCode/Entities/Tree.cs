using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameCore.Entities;

public class Tree : Entity
{
    private readonly Transform2 transform;

    private readonly Texture2D texture;
    public Tree(MainGame game, Texture2D texture, Vector2 pos) : base(game)
    {
        this.texture = texture;
        transform = new Transform2(pos);

    }

    public override void Update(float dt)
    {
    }
    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(
            texture, 
            new Rectangle(
                (int)transform.Position.X, 
                (int)transform.Position.Y, 
                (int)transform.Scale.X * texture.Width, 
                (int)transform.Scale.Y * texture.Height), 
            Color.White);
    }
}