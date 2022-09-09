using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using GameCore.Entities;
using Engine;

namespace GameCore.Screens;

public class MenuScreen : GameScreen
{
    Button playButton;
    OrthographicCamera camera;
    MainGame game => Game as MainGame;
    FPSCounter fpsCounter;
    public MenuScreen(MainGame game) : base(game)
    {
        camera = new OrthographicCamera(GraphicsDevice);
    }

    public override void LoadContent()
    {
        base.LoadContent();
        var font = Game.Content.Load<SpriteFont>(@"Fonts\consolas_22");
        playButton = new Button(Game as MainGame)
        {
            Color = Color.Red,
            HighlightColor = Color.Blue,
            TextColor = Color.Green,
            HighlightTextColor = Color.Blue,
            Filled = false,
            Font = font,
            Text = "Play",
            TextScale = 1f,
            Rect = new Rectangle(Game.GraphicsDevice.Viewport.Width/2-50, 100, 100, 40),
            TextOffset = new Point(16,6),            
        };

        playButton.OnClick += PlayButton_OnClick;


        fpsCounter = new FPSCounter(game)
        {
            Font = font,
        };
    }

    private void PlayButton_OnClick(object sender, ClickEventArgs e)
    {
        ScreenManager.LoadScreen(new CrawlScreen(game), new FadeTransition(GraphicsDevice, Color.Black, 2f));

    }

    public override void Update(GameTime gameTime)
    {        
        playButton.Update(gameTime.GetElapsedSeconds());

        fpsCounter.Tick(gameTime);
    }


    public override void Draw(GameTime gameTime)
    {
        game.SpriteBatch.Begin();
        playButton.Draw(game.SpriteBatch);
        game.SpriteBatch.DrawCircle(new CircleF(game.MouseState.Position, 4f), 10, Color.Green);

        fpsCounter.Draw(game.SpriteBatch);
        game.SpriteBatch.End();

    }

}