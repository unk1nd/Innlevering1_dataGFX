/*
 *  Innlevering i datamaskingrafikk
 *  6.september 2013 
 *  Del 2 av Mikael Bendiksen
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

namespace del2
{
    public class Del2 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;      // Representerer tegneflata.

        private BasicEffect effect;

        // Liste med vertekser
        private VertexPositionColor[] sider;
        private VertexPositionColor[] topp;
        private VertexPositionColor[] bunn;

        // WVP-matrisene
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        // Kameraposisjon
        private Vector3 cameraPosition = new Vector3(3.5f, 2.0f, 2.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        SpriteBatch spriteBatch;

        public Del2()
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

            Window.Title = "Kuben - del 2 - Mikael";

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
            // Vertexer for TriangleStrip
            sider = new VertexPositionColor[10];
            topp = new VertexPositionColor[4];
            bunn = new VertexPositionColor[4];
            effect.VertexColorEnabled = true;

            sider[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            sider[1].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            sider[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            sider[3].Position = new Vector3(0.5f, 0.5f, 0.5f);
            sider[4].Position = new Vector3(0.5f, -0.5f, -0.5f);
            sider[5].Position = new Vector3(0.5f, 0.5f, -0.5f);
            sider[6].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            sider[7].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            sider[8].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            sider[9].Position = new Vector3(-0.5f, 0.5f, 0.5f);

            sider[0].Color = Color.BurlyWood;
            sider[1].Color = Color.BurlyWood;
            sider[2].Color = Color.BurlyWood;
            sider[3].Color = Color.BurlyWood;
            sider[4].Color = Color.BurlyWood;
            sider[5].Color = Color.BurlyWood;
            sider[6].Color = Color.BurlyWood;
            sider[7].Color = Color.BurlyWood;
            sider[8].Color = Color.BurlyWood;
            sider[9].Color = Color.BurlyWood;

            topp[0].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            topp[1].Position = new Vector3(-0.5f, 0.5f, -0.5f);
            topp[2].Position = new Vector3(0.5f, 0.5f, 0.5f);
            topp[3].Position = new Vector3(0.5f, 0.5f, -0.5f);

            topp[0].Color = Color.SteelBlue;
            topp[1].Color = Color.SteelBlue;
            topp[2].Color = Color.SteelBlue;
            topp[3].Color = Color.SteelBlue;

            bunn[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            bunn[1].Position = new Vector3(-0.5f, -0.5f, -0.5f);
            bunn[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            bunn[3].Position = new Vector3(0.5f, -0.5f, -0.5f);

            bunn[0].Color = Color.MediumSpringGreen;
            bunn[1].Color = Color.MediumSpringGreen;
            bunn[2].Color = Color.MediumSpringGreen;
            bunn[3].Color = Color.MediumSpringGreen;
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
            rasterizerState1.FillMode = FillMode.WireFrame; // for å se kun streker av trekanten
            //rasterizerState1.FillMode = FillMode.Solid; // for å fylle med hel farge
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
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, sider, 0, 8, VertexPositionColor.VertexDeclaration);
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, topp, 0, 2, VertexPositionColor.VertexDeclaration);
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, bunn, 0, 2, VertexPositionColor.VertexDeclaration);

            }

            base.Draw(gameTime);
        }
    }
}
