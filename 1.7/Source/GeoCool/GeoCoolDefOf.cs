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

    [DefOf]
    public static class GeoCoolDefOf
    {
        static GeoCoolDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(GeoCoolDefOf));
        }

        public static ThingDef 
            GeoCool_GeothermalCooler,
            GeoCool_GeothermalFreezer;
    }
}
