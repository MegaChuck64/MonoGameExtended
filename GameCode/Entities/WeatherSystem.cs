using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Entities;

public class WeatherSystem
{

    public enum WeatherState
    {
        Sunny, 
        Cloudy,
        Raining, 
        Snowing
    }

    public WeatherState State { get; set; }

    private EntityManager entityManager;
    private FastRandom random = new();
    private float groundY;
    private MainGame game;
    public WeatherSystem(MainGame game, WeatherState currentState, float groundY, EntityManager entityManager)
    {
        State = currentState;
        this.game = game;
        this.groundY = groundY;
        this.entityManager = entityManager;
    }


    public void Update(float dt)
    {
        switch (State)
        {
            case WeatherState.Sunny:
                UpdateSunny(dt);
                break;
            case WeatherState.Cloudy:
                UpdateCloudy(dt);
                break;
            case WeatherState.Raining:
                UpdateRaining(dt);
                break;
            case WeatherState.Snowing:
                UpdateSnowing(dt);
                break;
            default:
                throw new NotImplementedException();                
        }
    }


    private void UpdateSunny(float dt)
    {

    }

    private void UpdateCloudy(float dt)
    {

    }
    float rainWait;
    private void UpdateRaining(float dt)
    {
        rainWait -= dt;
        if (rainWait <= 0f)
        {
            var vel = new Vector2(random.NextSingle(-20, 20), random.NextSingle(200, 500));
            var size = vel.Y / 100;

            entityManager.AddEntity(
                new Raindrop(
                    game,
                    new Vector2(random.NextSingle(-game.Width, game.Width * 2), random.NextSingle(-200, -10)),
                    vel,
                    groundY + 1000,
                    Color.Blue,
                    random.NextSingle(5f, 15f),
                    size)
                );

            rainWait = random.NextSingle(0.001f, 0.01f);
        }
    }

    float snowWait;
    private void UpdateSnowing(float dt)
    {
        snowWait -= dt;
        if (snowWait <= 0f)
        {
            var vel = new Vector2(random.NextSingle(-20, 20), random.NextSingle(200, 500));
            var size = (vel.Y / 100) * 0.5f;

            entityManager.AddEntity(
                new Raindrop(
                    game,
                    new Vector2(random.NextSingle(-game.Width, game.Width * 2), random.NextSingle(-200, -10)),
                    vel,
                    groundY,
                    Color.White,
                    random.NextSingle(3f, 10f),
                    size)
                );

            snowWait = random.NextSingle(0.001f, 0.005f);
        }

    }




    public void Draw(SpriteBatch sb)
    {
        switch (State)
        {
            case WeatherState.Sunny:
                DrawSunny(sb);
                break;
            case WeatherState.Cloudy:
                DrawCloudy(sb);
                break;
            case WeatherState.Raining:
                DrawRaining(sb);
                break;
            case WeatherState.Snowing:
                DrawSnowing(sb);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void DrawSunny(SpriteBatch sb)
    {

    }

    private void DrawCloudy(SpriteBatch sb)
    {

    }

    private void DrawRaining(SpriteBatch sb)
    {

    }

    private void DrawSnowing(SpriteBatch sb)
    {

    }



}