using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.Funny {
    public class FunnyModule : EverestModule {
        public static FunnyModule Instance { get; private set; }

        public override Type SettingsType => typeof(FunnyModuleSettings);
        public static FunnyModuleSettings Settings => (FunnyModuleSettings) Instance._Settings;

        public override Type SessionType => typeof(FunnyModuleSession);
        public static FunnyModuleSession Session => (FunnyModuleSession) Instance._Session;

        public FunnyModule() {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(FunnyModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(FunnyModule), LogLevel.Info);
#endif
        }

        public override void Load() {
            On.Celeste.Player.OnCollideH += Player_OnCollideH;
        }

        public override void Initialize()
        {
            base.Initialize();

            Logger.SetLogLevel("Funny", LogLevel.Debug);
        }

        private void Player_OnCollideH(On.Celeste.Player.orig_OnCollideH orig, Player self, CollisionData data) {
            Vector2 pos = self.Position;

            if (Settings.Enabled == false) {
                orig(self, data);
                return;
            }

            if (data.Hit.Hitbox != null) {
                Rectangle hitbox = data.Hit.Hitbox.Bounds;

                Logger.Log(LogLevel.Debug, "Funny", string.Format("{0}", data.Hit.));
                
                self.Position = data.TargetPosition;
                self.Position.Y = data.Hit.Collider.AbsoluteTop;
            }

            orig(self, data);
        }

        public override void Unload() {
            On.Celeste.Player.OnCollideH -= Player_OnCollideH;
        }
    }
}
