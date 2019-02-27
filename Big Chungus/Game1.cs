using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Big_Chungus
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {/*Author:  Maxwell Hazel and Max Bennett
         Class Purpose:  Runs a Monogame program with an image that can be moved with the arrow keys.  Also displays the location of the image.
         Caveats:  The location coordinates displayed in the program only track the top left corner of the image.*/
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        //Carrot carrot;
        Texture2D playerSprite;
        Texture2D dog;
        SpriteFont spriteFont;
        Platform platform;
        Platform wall;

        //player movement variables
        int hspd; //horizontal speed
        int vspd; //vertical speed
        int hacc; //horizontal acceleration
        int grav; //vertical acceleration from gravity
        int hmax; //maximum horizontal movespeed

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            
            hspd = 0;
            vspd = 0;
            hacc = 1;
            grav = 1;
            hmax = 7;
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

            // TODO: use this.Content to load your game content here
            
            
            

            //carrot.CarrotTexture = Content.Load<Texture2D>("Carrot");
            playerSprite = Content.Load<Texture2D>("BigChungus");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            dog = Content.Load<Texture2D>("SmilingPetDog");
            //carrot = new Carrot(200, 100);
            player = new Player(playerSprite, 0, 0);
            platform = new Platform(dog, 0, 231, 300, 4);
            wall = new Platform(dog, 231, 0, 40, 300);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ProcessInput();
            // TODO: Add your update logic here

            //update player position based on hspeed and vspeed
            player.XPos += hspd;
            player.YPos += vspd;
            base.Update(gameTime);
        }

        protected void ProcessInput()
        {
            KeyboardState input = Keyboard.GetState();

            //gravity
            if (!player.PlayerBox.Intersects(platform.PlatformBox))
            {
                vspd += grav;
            }
            else if(!input.IsKeyDown(Keys.Up))
            {
                vspd += 0;
            }

            //collision
            if (player.PlayerBox.Intersects(platform.PlatformBox) && !input.IsKeyDown(Keys.Up)&&player.YPos<=platform.YPos)
            {
                player.YPos = platform.YPos-player.Height;
            }
            if (player.PlayerBox.Intersects(wall.PlatformBox) && !input.IsKeyDown(Keys.Left))
            {
                player.XPos = wall.XPos - player.Width;
                hspd = 0;
            }
            if (player.PlayerBox.Intersects(wall.PlatformBox) && !input.IsKeyDown(Keys.Right))
            {
                player.XPos = wall.XPos + player.Width;
                hspd = 0;
            }

            //jump
            if (input.IsKeyDown(Keys.Up)&& player.PlayerBox.Intersects(platform.PlatformBox))
            {

                vspd = -16;
                
            }

            //left and right movement/deceleration
            if (input.IsKeyDown(Keys.Left) && hspd > -hmax)
            {
                hspd -= hacc;
            }
            else if (hspd < 0)
            {
                hspd += hacc;
            }
            if (input.IsKeyDown(Keys.Right) && hspd < hmax)
            {
                hspd += hacc;
            }
            else if (hspd > 0)
            {
                hspd -= hacc;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Draw
            spriteBatch.Draw(player.PlayerTexture, player.PlayerBox, Color.White);

            spriteBatch.DrawString(spriteFont, "such text, very picture, much input, wow", new Vector2(player.Width / 2, player.Height / 2), Color.White);

            spriteBatch.DrawString(spriteFont, player.XPos + ", " + player.YPos, new Vector2(0, 100), Color.White);
            spriteBatch.Draw(dog, platform.PlatformBox, Color.White);
            spriteBatch.Draw(dog, wall.PlatformBox, Color.White);


            // End the sprite batch
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
