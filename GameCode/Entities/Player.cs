using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;

namespace GameCore.Entities;

public class Player : Engine.Entity
{
    public Transform2 Transform { get; set; }

    public Inventory Inventory { get; set; }

    float moveSpeed = 100;

    Dictionary<string, Animation> Animations = new ();
    string currentAnimation;
    MainGame game => Game as MainGame;

    Animation anim => Animations[currentAnimation];
    Texture2D frame => anim.Frames[anim.CurrentFrame];

    public Player(MainGame game, Vector2 pos) : base(game)
    {
        Transform = new Transform2(pos);

        Animations.Add("Walk", new Animation(game, "Cowboy4_walk without gun", 4) { FPS = 4f, IsPlaying = true});
        Animations.Add("Idle", new Animation(game, "Cowboy4_idle without gun", 4) { FPS = 4f, IsPlaying = true});

        Animations.Add("Walk with gun", new Animation(game, "Cowboy4_walk with gun", 4) { FPS = 4f, IsPlaying = true });
        Animations.Add("Idle with gun", new Animation(game, "Cowboy4_idle with gun", 4) { FPS = 4f, IsPlaying = true });

        currentAnimation = "Idle";

        var hand = Game.Content.Load<Texture2D>(@"Sprites\hand");
        var gun = Game.Content.Load<Texture2D>(@"Sprites\revolver");
        Inventory = new Inventory(game)
        {
            InventorySlots = 6,
            Offset = 4,
            SlotColor = Color.Gray,
        };
        var invSlotWidth = 64f;
        Inventory.Transform =
            new Transform2(
                new Vector2(game.Width - ((Inventory.InventorySlots) * (invSlotWidth + Inventory.Offset) + (Inventory.Offset * 2)), 10),
                0f,
                new Vector2(invSlotWidth, invSlotWidth));
        Inventory.AddToInventory(0, hand);
        Inventory.AddToInventory(1, gun);

    }

    public override void Update(float dt)
    {
        var wasFacingLeft = anim.FacingLeft;
        var hasGun = Inventory.SelectedItem.Name.EndsWith("revolver");

        currentAnimation = hasGun ? "Idle with gun" : "Idle";
        anim.FacingLeft = wasFacingLeft;

        if (game.KeyState.IsKeyDown(Keys.D))
        {
            currentAnimation = hasGun ? "Walk with gun" : "Walk";

            Transform.Position += new Vector2(moveSpeed * dt, 0);

            if (Transform.Position.X > game.Width * 2 - (frame.Width))
                Transform.Position = new Vector2(game.Width * 2 - (frame.Width), Transform.Position.Y);
            
            anim.FacingLeft = false;
        }

        if (game.KeyState.IsKeyDown(Keys.A))
        {
            currentAnimation = hasGun ? "Walk with gun" : "Walk";

            Transform.Position += new Vector2(-moveSpeed * dt, 0);

            if (Transform.Position.X < -game.Width)
                Transform.Position = new Vector2(-game.Width, Transform.Position.Y);

            anim.FacingLeft = true;
        }

        anim.Transform = Transform;

        anim.Update(dt);
        Inventory.Update(dt);

    }

    public override void Draw(SpriteBatch sb)
    {
        var anim = Animations[currentAnimation];
        var frame = anim.Frames[anim.CurrentFrame];

        anim.Draw(sb);
    }

    public void DrawUI(SpriteBatch sb)
    {
        Inventory.Draw(game.SpriteBatch);
    }

}