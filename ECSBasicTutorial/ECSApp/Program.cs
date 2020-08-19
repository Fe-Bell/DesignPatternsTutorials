using ECSEngine;
using ECSEngine.Component;
using ECSEngine.Interface;
using ECSEngine.System;
using System;
using System.Threading.Tasks;

namespace ECSApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const float constantFrameTime = 1 /*seconds*/ / 60 /*frames*/;
            bool wasKeyPressed = false; 
            ConsoleKeyInfo keyInfo = default;          
            //Calls a basic async console monitor           
            ConsoleMonitor.KeyPressEventHandler onKeyPressEventHandlder = (args) => { wasKeyPressed = true; keyInfo = args.KeyInfo; };
            ConsoleMonitor.OnKeyPress += onKeyPressEventHandlder;
            ConsoleMonitor.Start();

            //Creates the engine and the scene
            Engine engine = BuildEngine();
            Scene scene = BuildScene0();

            while (true)
            {
                //Checks if the user has pressed a key
                if(wasKeyPressed)
                {
                    //If the user pressed ESC, then abort the program
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    //Change the State of entityA's sound
                    if (keyInfo.Key == ConsoleKey.P)
                    {
                        Entity entityA = scene.FindEntityByName<Entity>("entityA");
                        if(entityA != null)
                        {
                            Sound component = entityA.GetComponent<Sound>();
                            if(component != null)
                            {
                                component.PlaybackState = component.PlaybackState == Sound.PlaybackState_e.Playing ? Sound.PlaybackState_e.Paused : Sound.PlaybackState_e.Playing;
                            }
                        }
                    }
                    //Increase the mass of entityB's rigibody
                    if (keyInfo.Key == ConsoleKey.OemPlus)
                    {
                        Entity entity = scene.FindEntityByName<Entity>("entityB");
                        if (entity != null)
                        {
                            Rigidbody component = entity.GetComponent<Rigidbody>();
                            if (component != null)
                            {
                                component.Mass += 1.0f;
                            }
                        }
                    }
                    //Decrease the mass of entityB's rigibody
                    if (keyInfo.Key == ConsoleKey.OemMinus)
                    {
                        Entity entity = scene.FindEntityByName<Entity>("entityB");
                        if (entity != null)
                        {
                            Rigidbody component = entity.GetComponent<Rigidbody>();
                            if (component != null)
                            {
                                component.Mass -= 1.0f;
                            }
                        }
                    }

                    wasKeyPressed = false;
                }

                engine.SetActiveScene(scene);
                engine.UpdateSystems(constantFrameTime);

                //Delay just to facilitate visualization on the console
                System.Threading.Thread.Sleep(500);
            }

            ConsoleMonitor.Stop();
            ConsoleMonitor.OnKeyPress -= onKeyPressEventHandlder;
        }

        /// <summary>
        /// Builds a sample engine.
        /// </summary>
        /// <returns></returns>
        private static Engine BuildEngine()
        {
            Engine engine = new Engine();

            ISystem soundSystem = new SoundSystem() { IsEnabled = true, ID = 0, Name = "SampleSoundSystem" };
            engine.AddSystem(soundSystem);

            ISystem physicsSystem = new PhysicsSystem() { IsEnabled = true, ID = 1, Name = "SamplePhysicsSystem" };
            engine.AddSystem(physicsSystem);

            return engine;
        }
        /// <summary>
        /// Builds a sample Scene.
        /// </summary>
        /// <returns></returns>
        private static Scene BuildScene0()
        {
            Scene scene = new Scene() { ID = 0, IsEnabled = true, Name = "MySampleScene" };

            Entity entityA = new Entity() { Name = "entityA", ID = 0 };
            entityA.AddComponent(new Sound() { Name = "EntityASound", ID = 0, IsEnabled = true, Path = "//Assets//Sounds//BulletTheBlueSky.mp3", PlaybackState = Sound.PlaybackState_e.Playing});
            scene.AddEntity(entityA);

            Entity entityB = new Entity() { Name = "entityB", ID = 1 };
            entityB.AddComponent(new Sound() { Name = "EntityBSound", ID = 0, IsEnabled = false, Path = "//Assets//Sounds//WhosGonnaRideYourWildHorses.mp3" });
            entityB.AddComponent(new Rigidbody() { Name = "EntityBRigidbody", ID = 1, IsEnabled = true, IsFixed = true, Mass = 100.0f });
            scene.AddEntity(entityB);

            return scene;
        }
    }
}
