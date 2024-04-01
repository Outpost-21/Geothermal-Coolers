using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace GeoCool
{
    public class GeoCoolMod : Mod
    {
        public static GeoCoolMod mod;
        public static GeoCoolSettings settings;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public GeoCoolMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<GeoCoolSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            LogUtil.LogMessage($"{CurrentVersion} ::");

            if (Prefs.DevMode)
            {
                File.WriteAllText(VersionDir, CurrentVersion);
            }

            Harmony harmony = new Harmony("Neronix17.GeoCool.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Geothermal Coolers";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoOptionsCategoryContents(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {
            if (listing.ButtonText("GeoCool.RestoreDefaults".Translate()))
            {
                settings.cooler_heatPerSecond = -18f;
                settings.cooler_heatPushMinTemp = -5f;
                settings.cooler_fuelNeeded = true;
                settings.cooler_fuelConsumptionRate = 2f;

                settings.freezer_heatPerSecond = -75f;
                settings.freezer_heatPushMinTemp = -10f;
            }
            listing.Note("GeoCool.SettingsRequireRestart".Translate(), GameFont.Tiny);

            listing.LabelBacked("GeoCool.CoolerHeader".Translate(), Color.white);
            listing.AddLabeledSlider("GeoCool.HeatPerSecond".Translate(settings.cooler_heatPerSecond), ref settings.cooler_heatPerSecond,
                -100f, 0f, null, null, 1f);
            listing.AddLabeledSlider("GeoCool.MinTemperature".Translate(settings.cooler_heatPushMinTemp.ToStringTemperature()), ref settings.cooler_heatPushMinTemp, -50f, 20f, null, null, 1f);
            listing.GapLine();
            listing.CheckboxLabeled("GeoCool.FuelEnabled".Translate(), ref settings.cooler_fuelNeeded);
            if (settings.cooler_fuelNeeded)
            {
                listing.AddLabeledSlider("GeoCool.FuelConsumptionRate".Translate(settings.cooler_fuelConsumptionRate), ref settings.cooler_fuelConsumptionRate, 0.01f, 5f, null, null, 1f);
            }

            listing.LabelBacked("GeoCool.FreezerHeader".Translate(), Color.white);
            listing.AddLabeledSlider("GeoCool.HeatPerSecond".Translate(settings.freezer_heatPerSecond), ref settings.freezer_heatPerSecond, -200f, 0f, null, null, 1f);
            listing.AddLabeledSlider("GeoCool.MinTemperature".Translate(settings.freezer_heatPushMinTemp.ToStringTemperature()), ref settings.freezer_heatPushMinTemp, -70f, 0f, null, null, 1f);
        }
    }
}
