using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input;

namespace Engine;
public class Button : Entity
{
    public Rectangle Rect { get; set; }
    public Color Color { get; set; }
    public Color TextColor { get; set; }
    public Color HighlightColor { get; set; }
    public Color HighlightTextColor { get; set; }
    public SpriteFont Font { get; set; }
    public bool Filled { get; set; }
    public string Text { get; set; }
    public float TextScale { get; set; }
    public Point TextOffset { get; set; }
    public bool Hovering { get; set; }

    public delegate void OnClickEventHandler(object sender, ClickEventArgs e);
    public event OnClickEventHandler OnClick;

    public Button(BaseGame game) : base(game)
    {
    }


    public override void Update(float dt)
    {
        Hovering = Rect.Contains(Game.MouseState.Position);

        if (Hovering && Game.MouseState.WasButtonJustUp(MouseButton.Left))
            OnClick?.Invoke(this, new ClickEventArgs(MouseButton.Left));
    }

    public override void Draw(SpriteBatch sb)
    {
        if (Filled)
            sb.FillRectangle(Rect, Hovering ? HighlightColor : Color);
        else
            sb.DrawRectangle(Rect, Hovering ? HighlightColor : Color);

        sb.DrawString(Font, Text, (Rect.Location + TextOffset).ToVector2(),Hovering ? HighlightTextColor : TextColor, 0f, Vector2.Zero, TextScale, SpriteEffects.None, 0f);
    }

}

public class ClickEventArgs
{
    public ClickEventArgs(MouseButton mouseButton) { MouseButton = mouseButton; }
    public MouseButton MouseButton { get; }
}