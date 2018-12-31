using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
namespace GameConstructs {
    public static class Constants {
        private static string[] companyNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/CompanyNames.txt");
        private static string[] weaponNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/WeaponNames.txt");
        private static string[] shipNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/ShipNames.txt");
        private static string[] fcNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/FCNames.txt");
        private static string[] engineNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/EngineNames.txt");
        private static string[] sensorNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/SensorNames.txt");
        private static string[] powerNames = File.ReadAllLines(Application.dataPath + "/Resources/Text/PowerPlantNames.txt");

        public static float CalculateR(int hullSize) {
            return Mathf.Pow(3f / (4f * Mathf.PI) * hullSize, 1f / 3f);
        }

        public static int CalculateHyperdriveSize(int hullSize, float r) {
            float driveSizeFactor = 0.2f;
            int driveSize = Mathf.FloorToInt(Mathf.Min(1, driveSizeFactor * 1 / Mathf.Pow(r, 2f)));
            return driveSize;
        }

        public static Dictionary<WeaponType, int> CaliberSizeFactor = new Dictionary<WeaponType, int>() {
            {WeaponType.laser, 4},
            {WeaponType.railgun, 3}
        };

        public static Dictionary<PartType, string> PartTypeString = new Dictionary<PartType, string>() {
            {PartType.Engine, "Engine"},
            {PartType.Weapon, "Weapon"},
            {PartType.PowerPlant, "Power Plant"},
            {PartType.Sensor, "Sensor"},
            {PartType.FireControl, "Fire Control"},
        };

        public static Dictionary<PartType, Color> PartColor = new Dictionary<PartType, Color>() {
            {PartType.Engine, new Color(1.0f, 0.69f, 0.35f)},
            {PartType.Weapon, new Color(1.0f, 0.36f, 0.34f)},
            {PartType.PowerPlant, new Color(1.0f, 0.94f, 0.34f)},
            {PartType.Sensor, new Color(0.34f, 1f, 0.37f)},
            {PartType.FireControl, new Color(0.34f, 0.97f, 1f)},
        };

        public static Dictionary<int, float> TierDamagePerSize = new Dictionary<int, float>() {
            {0, 1.0f },
            {1, 1.2f},
            {2, 1.4f },
            {3, 1.6f},
            {4, 1.8f},
            {5, 2.0f},
            {6, 2.2f}
        };
        public static Dictionary<int, float> TierFireTimePerSize = new Dictionary<int, float>(){
            {0, 5.0f },
            {1, 4.5f},
            {2, 4.0f },
            {3, 3.5f},
            {4, 3.0f},
            {5, 2.5f},
            {6, 2.0f}
        };

        public static Dictionary<int, float> TierFireControlAccuracy = new Dictionary<int, float>(){
            {0, 1.0f },
            {1, 1.2f},
            {2, 1.4f },
            {3, 1.6f},
            {4, 1.8f},
            {5, 2.0f},
            {6, 2.2f}
        };

        public static Dictionary<int, string> TierAblativeArmorNames = new Dictionary<int, string>() {
            {0, "Hardened Steel"},
            {1, "Ceramic" },
            {2, "Polymer Composite"},
            {3, "Amorphous Metal"},
            {4, "Tensile Alloy"},
            {5, "Crystaline" },
            {6, "Condensed Osmium"}
        };

        public static Dictionary<int, string> TierEngineNames = new Dictionary<int, string>() {
            {0, "Liquid Oxygen"},
            {1, "Tripropellant" },
            {2, "Ion"},
            {3, "Plasma"},
            {4, "Magnetoplasma"},
            {5, "Radio Lensed" },
            {6, "Antimatter"}
        };

        public static string DamageToLaserWattage(int damage) {
            return (200 * damage).ToString() + "W";
        }

        public static string GetWeaponTypeName(int tier, int w) {
            if (w == 0) {
                switch (tier) {
                    case 0:
                        return "Solid State Laser";
                    case 1:
                        return "Metal Hallide Laser";
                    case 2:
                        return "Chemical Pumped Laser";
                    case 3:
                        return "Far UV Laser";
                    case 4:
                        return "X-Ray Laser";
                    case 5:
                        return "Far X-Ray Laser";
                    case 6:
                        return "Gamma Laser";
                    default:
                        Debug.LogWarning("Failed to find laser tier name");
                        return "";
                }
            } else if(w == 1) {
                switch (tier) {
                    case 0:
                        return "Tungsten Slug Railgun";
                    case 1:
                        return "Graphite Slug Railgun";
                    case 2:
                        return "Two Stage Railgun";
                    case 3:
                        return "Three Stage Railgun";
                    case 4:
                        return "Liquid Metal Railgun";
                    case 5:
                        return "Dual EMF Railgun";
                    case 6:
                        return "Relativistic Railgun";
                    default:
                        Debug.LogWarning("Failed to find railgun tier name");
                        return "";
                }
            }
            Debug.LogWarning("Failed to find weapontype for weaponType value" + w);
            return "";
        }

        public static string GetRandomCompanyName() {
            return companyNames[UnityEngine.Random.Range(0, companyNames.Length)];
        }

        public static string GetRandomShipName() {
            return shipNames[UnityEngine.Random.Range(0, shipNames.Length)];
        }

        public static string GetRandomWeaponModelName() {
            return weaponNames[UnityEngine.Random.Range(0, weaponNames.Length)];
        }

        public static string GetRandomFireControlModelName() {
            return fcNames[UnityEngine.Random.Range(0, fcNames.Length)];
        }

        public static string GetRandomEngineModelName() {
            return engineNames[UnityEngine.Random.Range(0, engineNames.Length)];
        }

        public static string GetRandomSensorModelName() {
            return sensorNames[UnityEngine.Random.Range(0, sensorNames.Length)];
        }

        public static string GetRandomPowerPlantModelName() {
            return powerNames[UnityEngine.Random.Range(0, powerNames.Length)];
        }
    }

    public enum WeaponType { laser, railgun};
    public enum SensorType { LowEnergy, HighEnergy, Gravitic};
    public enum PartType { Weapon, FireControl, Sensor, Engine, PowerPlant};
    public enum TweakableType { Slider, Dropdown };
}

