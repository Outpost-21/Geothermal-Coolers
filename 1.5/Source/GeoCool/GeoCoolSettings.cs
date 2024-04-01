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
    public class GeoCoolSettings : ModSettings
    {
        public bool verboseLogging = false;

        public float cooler_heatPerSecond = -18f;
        public float cooler_heatPushMinTemp = -5f;
        public bool cooler_fuelNeeded = true;
        public float cooler_fuelConsumptionRate = 2f;

        public float freezer_heatPerSecond = -75f;
        public float freezer_heatPushMinTemp = -10f;

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}
