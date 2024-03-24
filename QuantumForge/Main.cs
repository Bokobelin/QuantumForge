using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuantumForge.Engine;
using QuantumForge.Engine.Editor.EditorViews;
using QuantumForge.Engine.Frameworks.QuantumFramework.EngineCore;
using QuantumForge.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq; // Add this for LINQ

namespace QuantumForge
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D crossTexture;

        Entity player;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            player = new Entity();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //SET CONSTANTS
            Constants.contentManager = this.Content;

            Constants.graphicsDevice = this.GraphicsDevice;

            MonoBehaviour.AwakeAll();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Constants.spriteBatch = _spriteBatch;

            // TODO: use this.Content to load your game content here
            LoadAssets.LoadGame();

            // Load the texture from the path using ContentLoader
            crossTexture = ContentLoader.LoadTexture(LoadAssets.texturePaths["Cross.png"]);

            MonoBehaviour.StartAll();

            SpawnEntitiesFromHierarchy(Hierarchy._dataContext.SelectedScene);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ComponentSystem.Update();

            MonoBehaviour.UpdateAll();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Constants.spriteBatch.DrawString(Constants.defaultFont, "This is a test !", new Vector2(0, 0), Color.White);

            Constants.spriteBatch.Begin();

            ComponentSystem.Draw();

            MonoBehaviour.DrawAll();

            Constants.spriteBatch.End();

            base.Draw(gameTime);
        }

        // Method to spawn entities from the hierarchy
        private void SpawnEntitiesFromHierarchy(Scene hierarchy)
        {
            // Loop through all entities in the hierarchy
            foreach (var entity in hierarchy.GameObjects)
            {
                /*
                // Create a corresponding entity in the game
                Entity newEntity = new Entity();

                // Set properties of the new entity based on the hierarchy entity
                newEntity.Name = entity.Name;
                // Set position, rotation, scale, etc. as needed

                newEntity.AddComponent(new SpriteRenderer(crossTexture));
                
                */

                // Add the new entity to the game's entity management system
                // For example:
                entity.AddComponent(new SpriteRenderer(crossTexture));
                EntityManager.AddEntity(entity);
                Logger.LogInfo("Created new Entity : " + entity.Name);

                // Recursively spawn child entities
                /*
                if (entity.Children != null && entity.Children.Any())
                {
                    SpawnChildEntities(newEntity, entity.Children);
                }
                */
            }
        }

        // Method to recursively spawn child entities
        /*
        private void SpawnChildEntities(Entity parentEntity, List<Entity> childEntities)
        {
            foreach (var childEntity in childEntities)
            {
                // Create a new entity for the child
                Entity newChildEntity = new Entity();

                // Set properties of the new child entity
                newChildEntity.Name = childEntity.Name;
                // Set position, rotation, scale, etc. as needed

                // Add the child entity to the parent entity
                parentEntity.AddChild(newChildEntity);

                // Recursively spawn child entities of the child entity
                if (childEntity.Children != null && childEntity.Children.Any())
                {
                    SpawnChildEntities(newChildEntity, childEntity.Children);
                }
            }
        }
        */
    }
}
