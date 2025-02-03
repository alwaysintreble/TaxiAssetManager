﻿using System;
using System.IO;
using Chauffeur.Utils;
using Chauffeur.Utils.MenuUtils;
using TaxiAssetManager.Managers;

namespace TaxiAssetManager.AssetSwapping;

public class TaxiSkinMenu : CustomMenu
{
    public TaxiSkinMenu() : base("Change Taxi Skin")
    {
        try
        {
            LoadSkinButtons();
        }
        catch (Exception e)
        {
            if (e is DirectoryNotFoundException)
            {
                Directory.CreateDirectory(AssetManager.TaxiSkinsPath);
                foreach (var directory in Directory.GetDirectories(
                             Path.GetDirectoryName(AssetManagerMain.Instance.Info.Location) + "\\TaxiSkins"))
                {
                    var names = directory.Split('\\');
                    var dirName = names[names.Length - 1];
                    Directory.Move(directory, AssetManager.TaxiSkinsPath + $"\\{dirName}");
                }

                LoadSkinButtons();
            }

            AssetManagerMain.BepinLogger.LogError(e);
        }

        AddButton(new MenuButton("Reset Skin", AssetManager.Instance.ClearCustomSkin));
        AddButton(new MenuButton(LocalizationHelper.GoBackText, GoBack));
    }

    private void LoadSkinButtons()
    {
        foreach (var taxiSkinSet in Directory.GetDirectories(AssetManager.TaxiSkinsPath))
        {
            AssetManagerMain.BepinLogger.LogDebug(taxiSkinSet);
            var pathNames = taxiSkinSet.Split('\\');
            var name = pathNames[pathNames.Length - 1];
            AddButton(new MenuButton(name, _ => AssetManager.Instance.LoadSkin(taxiSkinSet)));
        }
    }
}