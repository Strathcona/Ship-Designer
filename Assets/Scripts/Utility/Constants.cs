﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace GameConstructs {

    //ship and part enums
    public enum WeaponType { Laser, Railgun };
    public enum SensorType { LowEnergy, HighEnergy, Gravitic };
    public enum ShieldType { Generator, Deflector};
    public enum PartType { Weapon, FireControl, Sensor, Engine, Reactor, Shield };
    public enum Orientation { Fore, Aft, Port, Starboard, Dorsal, Ventral, Internal };
    public enum PartSize { XS, S, M, L, XL};
    public enum ShipType { Carrier, Battlecruiser, Battleship, LightCruiser, HeavyCruiser, Destroyer, Gunboat, Fighter, Utility, Patrol, None};
    public enum TweakableType { Slider, Dropdown };

    //company enums
    public enum CompanyQuality { Speed, Quality, Cost, Prestige, Ethics, Quantity };
    public enum DepartmentType { Engineering, Finance, Legal, Facilities, Research}

    //galaxy entity enums
    public enum EntityFleetDoctrine { BorderPatrol, SmallNavy};

    public enum ConversationElementType { Text, Set, Choice, Branch, End }
    public enum CompanyTextKey { SpecialMitobahn, GenericOne }
    public enum ResearchNodeType {  Start, End, Mandatory, Optional};
    public enum GalaxyFeatureType { None, EntityCapital };

    public enum Gender { None, Nonbinary, Female, Male, ThirdGender}

    public static class Constants {
        public static int numberOfTiers = 8;

        public static Dictionary<PartSize, int> sizeFactor = new Dictionary<PartSize, int>() {
            { PartSize.XS, 1 },
            { PartSize.S, 2 },
            { PartSize.M, 4 },
            { PartSize.L, 8 },
            { PartSize.XL, 16 },
        };

        public static Dictionary<PartType, string> PartTypeString = new Dictionary<PartType, string>() {
            {PartType.Engine, "Engine"},
            {PartType.Weapon, "Weapon"},
            {PartType.Reactor, "Reactor"},
            {PartType.Sensor, "Sensor"},
            {PartType.FireControl, "Fire Control"},
            {PartType.Shield, "Shield"},
        };

        public static string GetPartDescriptionName(Part part) {
            return PartTierDescriptionStrings[part.Tier]+" "+ PartTypeString[part.partType];
        }
        private static Dictionary<int, string> PartTierDescriptionStrings = new Dictionary<int, string>() {
            {0, "Basic" },
            {1, "Advanced" },
            {2, "Turbo" },
            {3, "Hyper" },
            {4, "Ultra" },
            {5, "Quantum" },
            {6, "Meta" },
            {7, "Infinite" },
        };


        public static Dictionary<PartType, Color> PartColor = new Dictionary<PartType, Color>() {
            {PartType.Engine, new Color(1.0f, 0.69f, 0.35f)},
            {PartType.Weapon, new Color(1.0f, 0.36f, 0.34f)},
            {PartType.Reactor, new Color(1.0f, 0.94f, 0.34f)},
            {PartType.Sensor, new Color(0.34f, 1f, 0.37f)},
            {PartType.FireControl, new Color(0.34f, 0.97f, 1f)},
            {PartType.Shield, new Color(0.5f, 0.97f, 0.97f)},
        };

        public static Dictionary<PartType, string> ColoredPartTypeString = new Dictionary<PartType, string>() {
            {PartType.Engine, "<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.Engine])+">Engine</color>"},
            {PartType.Weapon, "<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.Weapon])+">Weapon</color>"},
            {PartType.Reactor,"<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.Reactor])+">Reactor</color>"},
            {PartType.Sensor, "<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.Sensor])+">Sensor</color>"},
            {PartType.FireControl, "<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.FireControl])+">Fire Control</color>"},
            {PartType.Shield, "<color=#"+ColorUtility.ToHtmlStringRGB(PartColor[PartType.Shield])+">Shield</color>"},
        };
               
        public static Dictionary<ShipType, string> ShipTypeString = new Dictionary<ShipType, string>() {
            {ShipType.Battlecruiser, "Battlecruiser" },
            {ShipType.Battleship, "Battleship" },
            {ShipType.Carrier, "Carrier" },
            {ShipType.Destroyer, "Destroyer" },
            {ShipType.Fighter, "Fighter" },
            {ShipType.Gunboat, "Gunboat" },
            {ShipType.HeavyCruiser, "Heavy Cruiser" },
            {ShipType.LightCruiser, "Light Cruiser" },
            {ShipType.None, "Ship" },
            {ShipType.Patrol, "Patrol Craft" },
            {ShipType.Utility, "Utility Craft" }
        };

        public static Dictionary<ShipType, float> FleetDoctrineCompositions(EntityFleetDoctrine doctrine){
            switch (doctrine) {
                case EntityFleetDoctrine.BorderPatrol:
                    return new Dictionary<ShipType, float>() {
                        {ShipType.Battlecruiser, 0.02f },
                        {ShipType.Battleship, 0.0f },
                        {ShipType.Carrier, 0.02f },
                        {ShipType.Destroyer, 0.25f },
                        {ShipType.Fighter, 0.0f },
                        {ShipType.Gunboat, 0.0f },
                        {ShipType.HeavyCruiser, 0.05f },
                        {ShipType.LightCruiser, 0.10f },
                        {ShipType.None, 0.0f },
                        {ShipType.Patrol, 0.50f },
                        {ShipType.Utility, 0.06f }
                    };
                case EntityFleetDoctrine.SmallNavy:
                    return new Dictionary<ShipType, float>() {
                        {ShipType.Battlecruiser, 0.00f },
                        {ShipType.Battleship, 0.7f },
                        {ShipType.Carrier, 0.07f },
                        {ShipType.Destroyer, 0.15f },
                        {ShipType.Fighter, 0.0f },
                        {ShipType.Gunboat, 0.0f },
                        {ShipType.HeavyCruiser, 0.25f },
                        {ShipType.LightCruiser, 0.30f },
                        {ShipType.None, 0.0f },
                        {ShipType.Patrol, 0.10f },
                        {ShipType.Utility, 0.06f }
                    };
                default:
                    return new Dictionary<ShipType, float>() {
                        {ShipType.Battlecruiser, 0.00f },
                        {ShipType.Battleship, 0.7f },
                        {ShipType.Carrier, 0.07f },
                        {ShipType.Destroyer, 0.15f },
                        {ShipType.Fighter, 0.0f },
                        {ShipType.Gunboat, 0.0f },
                        {ShipType.HeavyCruiser, 0.25f },
                        {ShipType.LightCruiser, 0.30f },
                        {ShipType.None, 0.0f },
                        {ShipType.Patrol, 0.10f },
                        {ShipType.Utility, 0.06f }
                    };
            }
        }

        public static string GetCompanyType(HashSet<CompanyQuality> qualities) {
            if (qualities.Contains(CompanyQuality.Speed)){
                return "Rapid Developers";
            }
            if (qualities.Contains(CompanyQuality.Quality)) {
                return "Relentless Perfectionists";
            }
            if (qualities.Contains(CompanyQuality.Cost)) {
                return "Ruthless Cost-Cutters";
            }
            if (qualities.Contains(CompanyQuality.Prestige)) {
                return "Honorable Collective";
            }
            if (qualities.Contains(CompanyQuality.Ethics)) {
                return "Ethical Enterprise";
            }
            if (qualities.Contains(CompanyQuality.Quantity)) {
                return "Bespoke Consortium";
            }
            return "Generic Megabrand";
        }

        public static Color GetRandomPastelColor() {
            byte R = (byte)((UnityEngine.Random.Range(0, 255) + 126) / 2);
            byte G = (byte)((UnityEngine.Random.Range(0, 255) + 126) / 2);
            byte B = (byte)((UnityEngine.Random.Range(0, 255) + 126) / 2);
            //averaged with grey to make it more pastel like and pleasing
            return new Color32(R, G, B, 255);            
        }
    }
}

