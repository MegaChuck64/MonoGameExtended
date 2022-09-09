using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using GameCore.Screens;
using MonoGame.Extended.Entities;

namespace GameCore;

public class MainGame : BaseGame
{
    public ScreenManager Screens { get; set; }
    public MainGame() : base()
    {
        Screens = new ScreenManager();
        Components.Add(Screens);
    }
    public override void Init()
    {
     
    }
    public override void Load(ContentManager content)
    {
        Screens.LoadScreen(new MenuScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
    }

}
