using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

namespace GameCore.Entities;

public class Animation : Engine.Entity
{
    public List<Texture2D> Frames { get; set; }

    public float FPS { get; set; }

    public int CurrentFrame { get; set; }

    public bool IsPlaying { get; set; }

    public Transform2 Transform { get; set; }

    public bool FacingLeft { get; set; }


    private float animTimer = 0f;    

    public Animation(MainGame game, string spriteName, int frameCount) : base(game)
    {
        Transform = new Transform2();
        Frames = new List<Texture2D>();
        for (int i = 0; i < frameCount; i++)
        {
            Frames.Add(game.Content.Load<Texture2D>(@$"Sprites\{spriteName}_{i}"));
        }
    }

    public override void Update(float dt)
    {
        if (!IsPlaying) return;

        animTimer += dt;

        if (animTimer > 1f/FPS)
        {
            animTimer = 0f;
            CurrentFrame++;
            if (CurrentFrame > Frames.Count - 1)
            { 
                CurrentFrame = 0; 
            }
        }

    }

    public Rectangle Bounds => new(
               (int)Transform.Position.X,
               (int)Transform.Position.Y,
               (int)Transform.Scale.X * Frames[CurrentFrame].Width,
               (int)Transform.Scale.Y * Frames[CurrentFrame].Height);

    public override void Draw(SpriteBatch sb)
    {
        sb.Draw(
           Frames[CurrentFrame],
           Bounds,
           null,
           Color.White,
           0f,
           FacingLeft ? new Vector2(Frames[CurrentFrame].Width * Transform.Scale.X/2, 0) : Vector2.Zero,
           FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
           0f);
    }

    
}