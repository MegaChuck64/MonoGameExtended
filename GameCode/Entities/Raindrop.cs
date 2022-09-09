using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameCore.Entities;

public class Raindrop : Engine.Entity
{
    private readonly Transform2 transform;
    private float lifetime;
    private float size;
    private float groundY;
    private Color tint;
    public Vector2 Position
    {
        get { return transform.Position; }
        set { transform.Position = value; }
    }
    public float Rotation
    {
        get { return transform.Rotation; }
        set { transform.Rotation = value; }
    }
    public Vector2 Velocity { get; set; }

    public Raindrop(MainGame game, Vector2 pos, Vector2 velocity, float groundYPos, Color color, float lifeTime = 1f, float diameter = 4f) : base(game)
    {
        lifetime = lifeTime;
        transform = new Transform2(pos);
        Velocity = velocity;
        size = diameter;
        groundY = groundYPos;
        tint = color;
    }


    public override void Update(float dt)
    {
        if (Position.Y < groundY)
            Position += Velocity * dt;

        lifetime -= dt;

        if (lifetime <= 0f)
        {
            Destroy();
        }
    }

    public override void Draw(SpriteBatch sb)
    {
        sb.DrawCircle(new CircleF(Position, size), 12, tint, size);
        //sb.FillRectangle(new RectangleF(Position.X, Position.Y, size, size), Color.White);
    }

}