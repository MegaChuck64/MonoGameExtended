using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameCore.Entities;

public class EventHistory : Engine.Entity
{

    public Rectangle ViewArea { get; set; }

    public EventHistory(MainGame game) : base(game)
    {
        var w = (game.Width / 6) - 20;
        var h = game.Height - (game.Height / 6);
        var x = 10;
        var y = 56;

        ViewArea = new Rectangle(x, y, w, h);
    }

    

    public override void Update(float dt)
    {
    }

    public override void Draw(SpriteBatch sb)
    {
        sb.DrawRectangle(ViewArea, Color.LightBlue, 2);

    }

}