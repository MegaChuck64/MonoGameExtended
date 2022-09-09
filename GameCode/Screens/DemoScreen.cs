using Engine;
using GameCore.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;

namespace GameCore.Screens;

public class DemoScreen : GameScreen
{
    MainGame game => Game as MainGame;


    SpriteFont font;
    OrthographicCamera camera;
    EntityManager entityManager;
    FPSCounter fpsCounter;
    Texture2D ground;
    Button backButton;
    WeatherSystem weatherSystem;
    Player player;    

    public DemoScreen(MainGame game) : base(game)
    {
        var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, game.Width, game.Height);
        camera = new OrthographicCamera(viewportAdapter);
        entityManager = new EntityManager();
    }

    public override void LoadContent()
    {
        base.LoadContent();
        font = Game.Content.Load<SpriteFont>(@"Fonts\consolas_22");
        ground = Game.Content.Load<Texture2D>(@"Sprites\ground");
        var treeTexture = Game.Content.Load<Texture2D>(@"Sprites\tree");
        var playerTexture = Game.Content.Load<Texture2D>(@"Sprites\player");


        entityManager.AddEntity(
            new Tree(
                game,
                treeTexture,
                new Vector2(100, game.Height - ground.Height - treeTexture.Height + 10)));
        weatherSystem = new WeatherSystem(game, WeatherSystem.WeatherState.Snowing, game.Height - ground.Height, entityManager);

        player = new Player(
            game,
            new Vector2(game.Width/2 - playerTexture.Width/2, game.Height - ground.Height - playerTexture.Height + 16f));
        //entityManager.AddEntity(player);

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
        //dont add to entity system, we want to draw it last
        //entityManager.AddEntity(fpsCounter);
    }

    private void BackButton_OnClick(object sender, ClickEventArgs e)
    {
        ScreenManager.LoadScreen(new MenuScreen(game), new FadeTransition(GraphicsDevice, Color.Black, 2f));

    }

    public override void Update(GameTime gameTime)
    {
        player.Update(gameTime.GetElapsedSeconds());
        entityManager.Update(gameTime);
        weatherSystem.Update(gameTime.GetElapsedSeconds());
        backButton.Update(gameTime.GetElapsedSeconds());
        fpsCounter.Tick(gameTime);

        if (game.KeyState.WasKeyJustDown(Keys.R))
        {
            weatherSystem.State = WeatherSystem.WeatherState.Raining;
        }
        if (game.KeyState.WasKeyJustDown(Keys.S))
        {
            weatherSystem.State = WeatherSystem.WeatherState.Snowing;
        }


        camera.Position = new Vector2(player.Transform.Position.X - game.Width / 2, camera.Position.Y);

        if (camera.Position.X > game.Width)
            camera.Position = new Vector2(game.Width, camera.Position.Y);

        if (camera.Position.X < -game.Width)
            camera.Position = new Vector2(-game.Width, camera.Position.Y);
        
    }

    public override void Draw(GameTime gameTime)
    {
        var transformMatrix = camera.GetViewMatrix();
        game.SpriteBatch.Begin(transformMatrix: transformMatrix);

        // game.SpriteBatch.Begin();

        //ground on top of other entities
        game.SpriteBatch.Draw(ground, new Vector2(-ground.Width, game.Height - ground.Height), Color.White);
        game.SpriteBatch.Draw(ground, new Vector2(0, game.Height - ground.Height), Color.White);
        game.SpriteBatch.Draw(ground, new Vector2(ground.Width, game.Height - ground.Height), Color.White);

        entityManager.Draw((Game as MainGame).SpriteBatch);
        player.Draw(game.SpriteBatch);

        game.SpriteBatch.End();



        //UI        
        game.SpriteBatch.Begin();

        player.DrawUI(game.SpriteBatch);
        fpsCounter.Draw(game.SpriteBatch);
        backButton.Draw(game.SpriteBatch);

        //mouse pointer on top of everything
        game.SpriteBatch.DrawCircle(new CircleF(game.MouseState.Position, 4f), 10, Color.Green);

        game.SpriteBatch.End();
    }

}