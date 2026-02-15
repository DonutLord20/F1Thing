using System;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F1Thing2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private bool Start;
    private bool Timing;
    private bool Finished;
    private bool Delaying;
    private bool Driving;
    private double Time;
    private int Delay;
    private int Count;
    private const int LIGHT_DELAY = 120;
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
        Start = false;
        Timing = false;
        Delaying = false;
        Finished = false;
        Driving = false;
        Time = 0;
        Delay = Rand.Next(5,601);
        Count = 0;

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
       KeyboardState MyKey = Keyboard.GetState();

       if (MyKey.IsKeyDown(Keys.Enter) && Start == false) {Start = true;}

       if (Start)
       {
            if (!Timing && !Delaying && !Finished)
            {
                Count++;

                if (Count % LIGHT_DELAY == 0) {SourceLights.X += 32;}
                
                if (Count == LIGHT_DELAY * 3) {Delaying = true; Count = 0;}
            }
            else if (!Timing && Delaying)
            {
                Count++;

                if (Count == Delay)
                {
                    Timing = true;
                    Delaying = false;
                    Count = 0;
                    SourceLights.X += 32;
                }
            }
            else if (Timing)
            {
                Count++;
                if (Count % 7.5 == 0) {Time += 0.125;}

                if (!Finished && MyKey.IsKeyDown(Keys.Space))
                {
                    Timing = false;
                    Finished = true;
                    Driving = true;
                }
            }

            if (Driving) {Car.X += 15;}
       }
       
        // TODO: Add your update logic here
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        if (!Start) {_spriteBatch.DrawString(GameFont,"Press Enter to Start",new Vector2(400,200),Color.White);}
        else
        {
        _spriteBatch.Draw(TBackGround,BackGround,Color.White);
        _spriteBatch.Draw(TCar,Car,Color.White);
        _spriteBatch.Draw(TLights,new Vector2(Lights.X,Lights.Y),SourceLights,Color.White,0f,Vector2.Zero,3f,SpriteEffects.None,0f);
        _spriteBatch.DrawString(GameFont,$"Time: {Time}",Vector2.Zero,Color.Black);
        }
        _spriteBatch.End();
        // TODO: Add your drawing code here


        base.Draw(gameTime);
    }
}
