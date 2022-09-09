using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Collections.Generic;

namespace GameCore.Entities;

public class Inventory : Engine.Entity
{
    public Transform2 Transform { get; set; }
    public int InventorySlots { get; set; }
    public int Offset { get; set; }
    public Color SlotColor { get; set; }
    public Color SelectedSlotColor { get; set; }
    public int SelectedSlot { get; set; }
    public Dictionary<int, Texture2D > Items { get; set; } = new ();
    public Texture2D SelectedItem => Items.ContainsKey(SelectedSlot) ? Items [SelectedSlot] : null;

    MainGame game => Game as MainGame;
    public Inventory(MainGame game) : base(game)
    {
        Transform = new Transform2();
        SlotColor = Color.White;
        SelectedSlotColor = Color.Yellow;
    }
    
    public override void Update(float dt)
    {
        if (game.KeyState.WasKeyJustDown(Keys.Left))
        {
            var nextSlot = SelectedSlot;
            nextSlot--;
            if (nextSlot< 0)
                nextSlot = InventorySlots - 1;

            if (Items.ContainsKey(nextSlot))
                SelectedSlot = nextSlot;
        }

        if (game.KeyState.WasKeyJustDown(Keys.Right))
        {
            var nextSlot = SelectedSlot;
            nextSlot++;
            if (nextSlot >= InventorySlots)
                nextSlot = 0;

            if (Items.ContainsKey(nextSlot))
                SelectedSlot = nextSlot;
        }
    }

    public override void Draw(SpriteBatch sb)
    {
        for (int i = 0; i < InventorySlots; i++)
        {
            var rect = new RectangleF(Transform.Position.X + ((Transform.Scale.X + Offset) * i), Transform.Position.Y, Transform.Scale.X, Transform.Scale.Y);
            sb.DrawRectangle(rect, i == SelectedSlot ? SelectedSlotColor : SlotColor, 2f);

            if (Items.ContainsKey(i))
                sb.Draw(Items[i], rect.ToRectangle(), Color.White);
        }
    }

    public void AddToInventory(int slot, Texture2D texture)
    {
        Items[slot] = texture;
    }

}