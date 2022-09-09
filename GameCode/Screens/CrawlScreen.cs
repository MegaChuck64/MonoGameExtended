using Engine;
using GameCore.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;

namespace GameCore.Screens;
public class CrawlScreen : GameScreen
{
    MainGame game => Game as MainGame;

    SpriteFont font;
    OrthographicCamera camera;
    EntityManager entityManager;
    FPSCounter fpsCounter;
    Button backButton;
    Map map;
    public CrawlScreen(MainGame game) : base(game)
    {

        entityManager = new EntityManager();
    }

    public override void LoadContent()
    {
        base.LoadContent();
        font = Game.Content.Load<SpriteFont>(@"Fonts\consolas_22");

        fpsCounter = new FPSCounter(game)
        {
            Font = font,
        };

        backButton = new Button(game)
        {
            Color = Color.Red,
            HighlightColor = Color.Blue,
            TextColor = Color.Green,
            HighlightTextColor = Color.Blue,
            Filled = false,
            Font = font,
            Text = "Back",
            TextScale = 1f,
            Rect = new Rectangle(10, 10, 100, 40),
            TextOffset = new Point(17, 6),
        };

        backButton.OnClick += BackButton_OnClick;

       
        var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, game.Width, game.Height);
        camera = new OrthographicCamera(viewportAdapter);
        map = new Map(game);
        entityManager.AddEntity(map);
        //eventHistory = new EventHistory(game);
        //entityManager.AddEntity(eventHistory);
    }

    private void BackButton_OnClick(object sender, ClickEventArgs e)
    {
        ScreenManager.LoadScreen(new MenuScreen(game), new FadeTransition(GraphicsDevice, Color.Black, 2f));
    }

    public override void Update(GameTime gameTime)
    {
        entityManager.Update(gameTime);
        backButton.Update(gameTime.GetElapsedSeconds());
        fpsCounter.Tick(gameTime);

    }

    public override void Draw(GameTime gameTime)
    {
        var transformMatrix = camera.GetViewMatrix();
        game.SpriteBatch.Begin(transformMatrix: transformMatrix);

        entityManager.Draw((Game as MainGame).SpriteBatch);
        
        game.SpriteBatch.End();



        //UI        
        game.SpriteBatch.Begin();

        fpsCounter.Draw(game.SpriteBatch);
        backButton.Draw(game.SpriteBatch);

        game.SpriteBatch.DrawCircle(new CircleF(game.MouseState.Position, 4f), 10, Color.Green);

        game.SpriteBatch.End();
    }
}
