using BepInEx;

namespace Blasphemous.Andalusian;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.4.1")]
internal class Main : BaseUnityPlugin
{
    public static Andalusian Andalusian { get; private set; }

    private void Start()
    {
        Andalusian = new Andalusian();
    }
}
