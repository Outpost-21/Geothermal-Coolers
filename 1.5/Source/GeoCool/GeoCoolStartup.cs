using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace GeoCool
{
    [StaticConstructorOnStartup]
    public static class GeoCoolStartup
    {
        static GeoCoolStartup()
        {
            GeoCoolSettings s = GeoCoolMod.settings;
            ApplyCoolerStats(s);
            ApplyFreezerStats(s);
        }

        public static void ApplyCoolerStats(GeoCoolSettings s)
        {
            CompProperties_HeatPusher pusherComp = GeoCoolDefOf.GeoCool_GeothermalCooler.GetCompProperties<CompProperties_HeatPusher>();
            if(pusherComp != null)
            {
                pusherComp.heatPerSecond = s.cooler_heatPerSecond;
                pusherComp.heatPushMinTemperature = s.cooler_heatPushMinTemp;
            }
            CompProperties_Refuelable refuelComp = GeoCoolDefOf.GeoCool_GeothermalCooler.GetCompProperties<CompProperties_Refuelable>();
            if (!s.cooler_fuelNeeded)
            {
                GeoCoolDefOf.GeoCool_GeothermalCooler.comps.Remove(refuelComp);
            }
            else
            {
                refuelComp.fuelConsumptionRate = s.cooler_fuelConsumptionRate;
            }
        }

        public static void ApplyFreezerStats(GeoCoolSettings s)
        {
            CompProperties_HeatPusher pusherComp = GeoCoolDefOf.GeoCool_GeothermalFreezer.GetCompProperties<CompProperties_HeatPusher>();
            if (pusherComp != null)
            {
                pusherComp.heatPerSecond = s.freezer_heatPerSecond;
                pusherComp.heatPushMinTemperature = s.freezer_heatPushMinTemp;
            }
        }
    }
}
