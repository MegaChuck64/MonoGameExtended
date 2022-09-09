using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameCore.Entities;

public class FPSCounter : Entity
{
    int frameRate = 0;
    int frameCounter = 0;
    TimeSpan elapsedTime = TimeSpan.Zero;
    public SpriteFont Font { get; set; }

    public FPSCounter(MainGame game) : base(game)
    {
    }

    public override void Update(float dt)
    {
        
    }

    public void Tick(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime;

        if (elapsedTime > TimeSpan.FromSeconds(1))
        {
            elapsedTime -= TimeSpan.FromSeconds(1);
            frameRate = frameCounter;
            frameCounter = 0;
        }
    }

    public override void Draw(SpriteBatch sb)
    {
        frameCounter++;
        var fps = $"FPS: {frameRate}";
        sb.DrawString(Font, fps, new Vector2(4, sb.GraphicsDevice.Viewport.Height - 40), Color.Yellow);
    }
}