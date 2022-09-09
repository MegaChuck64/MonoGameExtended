using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Input;
using MonoGame.Extended.Sprites;

namespace Engine
{
    public abstract class BaseGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch { get; set; }
        public KeyboardStateExtended KeyState { get; private set; }
        public MouseStateExtended MouseState { get; private set; }

        public Color BackgroundColor { get; set; } = Color.Black;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public BaseGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Init();
            if (GraphicsDevice.DisplayMode.Height >= 1080)
            {
                Width = 1280;
                Height = 720;
            }
            else
            {
                Width = 800;
                Height = 450;
            }

            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            IsFixedTimeStep = true;
            IsMouseVisible = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }



        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Load(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            KeyState = KeyboardExtended.GetState();
            MouseState = MouseExtended.GetState();
  
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);

            base.Draw(gameTime);
        }

        public abstract void Load(ContentManager content);
        public abstract void Init();
    }
}
