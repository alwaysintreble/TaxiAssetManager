using BepInEx;
using BepInEx.Logging;
using Chauffeur.Managers;
using TaxiAssetManager.Managers;

namespace TaxiAssetManager;

[BepInDependency("com.alwaysintreble.Chauffeur")]
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class AssetManagerMain : BaseUnityPlugin
{
    public const string PluginGuid = "com.alwaysintreble.TaxiAssetManager";
    public const string PluginName = "TaxiAssetManager";
    public const string PluginVersion = "0.2.0";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    public static ManualLogSource BepinLogger;
    public static AssetManagerMain Instance;

    private void Awake()
    {
        // AssetManagerMain startup logic
        BepinLogger = Logger;
        Instance = this;

        BepinLogger.LogMessage($"{ModDisplayInfo} loaded!");
        MenuManager.AddButtons += () =>
        {
            var assetManager = new AssetManager();
            assetManager.CreateAssetSwapMenus();
        };
    }
}