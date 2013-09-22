/*
 *  Innlevering i datamaskingrafikk
 *  6.september 2013 
 *  Del 1 av Mikael Bendiksen
 *  
*/

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

namespace innlevering1_dataGFX
{
    public class Del1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;      // Representerer tegneflata.

        private BasicEffect effect;

        // Liste med vertekser
        private VertexPositionColor[] vertices;

        // WVP-matrisene
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        // Kameraposisjon
        private Vector3 cameraPosition = new Vector3(3.5f, 2.0f, 2.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        SpriteBatch spriteBatch;

        public Del1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = new ContentManager(this.Services);
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitDevice();
            InitCamera();
            InitVertices();
        }

        private void InitDevice()
        {
            device = graphics.GraphicsDevice;

            // Setter størrelse på framebuffer:
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Window.Title = "Kuben - del 1 - Mikael";

            // Initialiserer Effect-objektet:
            effect = new BasicEffect(graphics.GraphicsDevice);
        }

        private void InitCamera()
        {
            // Projeksjon
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            //Oppretter view-matrisa
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

            // Oppretter projeksjonsmatrisa
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.01f, 1000.0f, out projection);

            // Gir matrisene til shader
            effect.Projection = projection;
            effect.View = view;
        }

        // setter vertexer til trekanter og setter farger 
        private void InitVertices()
        {
            vertices = new VertexPositionColor[36];
            effect.VertexColorEnabled = true;
            
            // Front
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[1].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[3].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[4].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[5].Position = new Vector3(0.5f, -0.5f, 0.5f);
            
            // Farger front
            vertices[0].Color = Color.BurlyWood;
            vertices[1].Color = Color.BurlyWood;
            vertices[2].Color = Color.BurlyWood;
            vertices[3].Color = Color.BurlyWood;
            vertices[4].Color = Color.BurlyWood;
            vertices[5].Color = Color.BurlyWood;

            // Bak
            vertices[6].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            vertices[7].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[8].Position = new Vector3(0.5f, -0.5f, -0.5f);
            vertices[9].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[10].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[11].Position = new Vector3(0.5f, -0.5f, -0.5f);
            
            // Farge bak
            vertices[6].Color = Color.Red;
            vertices[7].Color = Color.Red;
            vertices[8].Color = Color.Red;
            vertices[9].Color = Color.Red;
            vertices[10].Color = Color.Red;
            vertices[11].Color = Color.Red;

            // Topp
            vertices[12].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[13].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[14].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[15].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[16].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[17].Position = new Vector3(0.5f, 0.5f, 0.5f);
           
            // Farge top
            vertices[12].Color = Color.SteelBlue;
            vertices[13].Color = Color.SteelBlue;
            vertices[14].Color = Color.SteelBlue;
            vertices[15].Color = Color.SteelBlue;
            vertices[16].Color = Color.SteelBlue;
            vertices[17].Color = Color.SteelBlue;

            // Bunn
            vertices[18].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[19].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            vertices[20].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[21].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            vertices[22].Position = new Vector3(0.5f, -0.5f, -0.5f);
            vertices[23].Position = new Vector3(0.5f, -0.5f, 0.5f);
            
            // Farge bunn
            vertices[18].Color = Color.Green;
            vertices[19].Color = Color.Green;
            vertices[20].Color = Color.Green;
            vertices[21].Color = Color.Green;
            vertices[22].Color = Color.Green;
            vertices[23].Color = Color.Green;

            // Høyreside
            vertices[24].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[25].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[26].Position = new Vector3(0.5f, -0.5f, -0.5f);
            vertices[27].Position = new Vector3(0.5f, 0.5f, 0.5f);
            vertices[28].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[29].Position = new Vector3(0.5f, -0.5f, -0.5f);
            
            // Farge høyreside
            vertices[24].Color = Color.Azure;
            vertices[25].Color = Color.Azure;
            vertices[26].Color = Color.Azure;
            vertices[27].Color = Color.Azure;
            vertices[28].Color = Color.Azure;
            vertices[29].Color = Color.Azure;

            // Venstreside
            vertices[30].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[31].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[32].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            vertices[33].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[34].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            vertices[35].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            
            // Farge venstreside
            vertices[30].Color = Color.DarkSlateGray;
            vertices[31].Color = Color.DarkSlateGray;
            vertices[32].Color = Color.DarkSlateGray;
            vertices[33].Color = Color.DarkSlateGray;
            vertices[34].Color = Color.DarkSlateGray;
            vertices[35].Color = Color.DarkSlateGray;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            //rasterizerState1.FillMode = FillMode.WireFrame; // for å se kun streker av trekanten
            rasterizerState1.FillMode = FillMode.Solid;
            device.RasterizerState = rasterizerState1;

            device.Clear(Color.Black);

            // Setter world
            world = Matrix.Identity;
            // Setter world-matrisa på effect-objektet (verteks-shaderen)
            effect.World = world;

            // Starter tegning - må bruke effect-objektet
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall
                // primitiver (her 1 siden verteksene beskriver en trekant)
                device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 12, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);
        }
    }
}
