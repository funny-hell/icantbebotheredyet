namespace Celeste.Mod.Funny {
    public class FunnyModuleSettings : EverestModuleSettings {
        [SettingInGame(true)]
        public bool Enabled { get; set; } = true;
    }
}
