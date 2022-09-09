using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;


namespace GameCore.Entities;

public class Map : Engine.Entity
{
    public int XOffset { get; set; } = 0;
    public int YOffset { get; set; } = 0;
    public int ViewWidth { get; set; } = 26;
    public int ViewHeight { get; set; } = 20;
    public int ViewX { get; set; } = 120;
    public int ViewY { get; set; } = 10;
    public int TileSize { get; set; } = 32;

    public Color[,] Tiles;
    
    public Map(MainGame game) : base(game)
    {
        //ViewWidth = game.Width - (game.Width / 3);
        //ViewHeight = game.Height - (game.Height / 6);
        
        var tw = 50;
        var th = 50;
        Tiles = new Color[tw, th];
        var flip = false;
        for (var tx = 0; tx < tw; tx++)
        {
            for (var ty = 0; ty < th; ty++)
            {
                Tiles[tx, ty] = flip ? Color.Green : Color.Pink;
                flip = !flip;
            }
            flip = !flip;
        }        
    }

    public override void Update(float dt)
    {
        if (Game.KeyState.WasKeyJustDown(Keys.Right))
            XOffset++;

        if (Game.KeyState.WasKeyJustDown(Keys.Left))
            XOffset--;


        if (Game.KeyState.WasKeyJustDown(Keys.Up))
            YOffset--;

        if (Game.KeyState.WasKeyJustDown(Keys.Down))
            YOffset++;
    }


    public override void Draw(SpriteBatch sb)
    {
        for (int x = 0; x < ViewWidth; x++)
        {
            for (int y= 0;y < ViewHeight; y++)
            {
                var xIndex = x + XOffset;
                var yIndex = y + YOffset;

                if (xIndex < Tiles.GetLength(0) - 1 && 
                    yIndex < Tiles.GetLength(1) - 1 && 
                    xIndex >= 0 && 
                    yIndex >= 0)
                        sb.FillRectangle(
                            new Rectangle(
                                ViewX + (TileSize * x), 
                                ViewY + (TileSize * y), 
                                TileSize, 
                                TileSize), 
                            Tiles[xIndex, yIndex]);
            }
        }
    }

}