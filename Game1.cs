using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ShipPrototype.Services;
using ShipPrototype.Components;


namespace ShipPrototype
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameEntity player;
        GameEntity world;
        GameEntity test;
        Point screen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            screen = new Point(800, 600);
            graphics.PreferredBackBufferWidth = screen.X;
            graphics.PreferredBackBufferHeight = screen.Y;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Locator.provide(InputHandler.init());
            Locator.provide(ComponentManager.init());
            Locator.provide(new Random());
            Locator.provide(new ControlManager());
            Locator.getInputHandler().addKeyReleaseObserver(Locator.getControlManager().keyRelease);
            Locator.getInputHandler().addMouseReleaseObserver(Locator.getControlManager().mouseUp);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Locator.provide(ScreenPrinter.init(Content.Load<SpriteFont>("SpriteFont1")));
            Locator.provide(TextureManager.init(GraphicsDevice, Content));

            world = new GameEntity();
            world.spatial = new SpatialComponent(world, new Vector2(400, 300), 0, Vector2.One);
            Ship.loadTextures();

            Locator.provide(Ship.init(world));
            Locator.getControlManager().forceState(new ControlStates.Selector());

            player = new GameEntity();
            player.spatial = new Components.SpatialComponent(player, Locator.getShip().entity_.spatial, Vector2.Zero, 0, Vector2.One);
            player.render = new Components.RenderComponent(player, Locator.getTextureManager().loadTexture("person32"), 1, new Vector2(16), Color.White);
            player.controller = new Components.ControllerComponent(player);
            Locator.getComponentManager().addEntity(player);

            test = new GameEntity();
            test.spatial = new SpatialComponent(test, Locator.getShip().entity_.spatial, Vector2.Zero, 0, new Vector2(3));
            test.render = new RenderComponent(test, -1, 0, new Vector2(0.5f), Color.White);
            Locator.getComponentManager().addEntity(test);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;

            Locator.getInputHandler().update(elapsed);
            Locator.getComponentManager().update(elapsed);
            Locator.getControlManager().update(elapsed);

            world.spatial.translation_ = new Vector2((screen.X / 2) - player.spatial.translation_.X, (screen.Y / 2) - player.spatial.translation_.Y);

            MouseState ms = Mouse.GetState();
            Vector2 mous = new Vector2(ms.X, ms.Y);
            Vector2 spot = Locator.getShip().entity_.spatial.worldToLocal(mous);
            test.spatial.translation_ = spot;
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Locator.getComponentManager().render(spriteBatch);
            Locator.getControlManager().render(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
