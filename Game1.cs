using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F1Thing2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool Counting;
    private bool Finished;
    private bool Drive;
    private bool StartLights;
    
    private int CountDown;
    private int Count;
    private double Time;
    private int Delay;
    private Random Rand = new Random();
    private SpriteFont GameFont;
    private Texture2D TCar;
    private Rectangle Car;

    private Texture2D TBackGround;
    private Rectangle BackGround;

    private Texture2D TLights;
    private Rectangle Lights;
    private Rectangle SourceLights;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Counting = false;
        Finished = false;
        Drive = false;
        StartLights = false;
        CountDown = 0;
        Count = 0;
        Time = 0;
        Delay = 1;
         
        Car = new Rectangle(0,400,200,60);
        BackGround = new Rectangle(0,0,800,480);
        Lights = new Rectangle(700,8,28,8);
        SourceLights = new Rectangle(0,0,28,8);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        GameFont = Content.Load<SpriteFont>("GameFont");
        TCar = Content.Load<Texture2D>("Car");
        TBackGround = Content.Load<Texture2D>("BackGround");
        TLights = Content.Load<Texture2D>("Lights");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        KeyboardState MyKey = Keyboard.GetState();

        if (Delay == 0 && !Finished) {StartLights = true;}
        else {Delay = Rand.Next(0,500);}

        if (Counting && MyKey.IsKeyDown(Keys.Space))
        {
            Counting = false;
            Finished = true;
            Drive = true;
        }
        else if (Counting && !Finished)
        {
            Count++;
            if (Count % 7.5 == 0)
            {
                Time += 0.125;
            }
        }

        if (Drive) {Car.X += 15;}

        if (StartLights)
        {
            CountDown++;

            if (CountDown % 5 == 0)
            {
                SourceLights.X += 32;


                if (CountDown == 20)
                {
                    StartLights = false;
                    Counting = true;
                }
            }
        }

        if (Finished && Car.X > 500) {Initialize();}
        // TODO: Add your update logic here
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(TBackGround,BackGround,Color.White);
        _spriteBatch.Draw(TCar,Car,Color.White);
        _spriteBatch.Draw(TLights,new Vector2(Lights.X,Lights.Y),SourceLights,Color.White,0f,Vector2.Zero,3f,SpriteEffects.None,0f);
        _spriteBatch.DrawString(GameFont,$"Time: {Time}",Vector2.Zero,Color.Black);
        _spriteBatch.End();
        // TODO: Add your drawing code here


        base.Draw(gameTime);
    }
}
