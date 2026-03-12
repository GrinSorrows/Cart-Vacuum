using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CartAutoCollect
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class CartAutoCollectPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.community.cartautocollect";
        public const string PluginName = "CartAutoCollect";
        public const string PluginVersion = "1.0.0";

        public static ConfigEntry<bool> ModEnabled;
        public static ConfigEntry<float> CollectionRadius;
        public static ConfigEntry<float> CollectionInterval;
        public static ConfigEntry<bool> OnlyMatchingItems;
        public static ConfigEntry<bool> CollectWhenPulling;

        private readonly Harmony harmony = new Harmony(PluginGUID);

        private void Awake()
        {
            ModEnabled = Config.Bind("General", "ModEnabled", true,
                "Enable or disable CartAutoCollect.");

            CollectionRadius = Config.Bind("Settings", "CollectionRadius", 8f,
                "Radius (in metres) around the cart to vacuum up items.");

            CollectionInterval = Config.Bind("Settings", "CollectionInterval", 1f,
                "How often (in seconds) the cart checks for nearby items.");

            OnlyMatchingItems = Config.Bind("Settings", "OnlyMatchingItems", true,
                "If true, only collect item types already present in the cart. If false, collect everything.");

            CollectWhenPulling = Config.Bind("Settings", "CollectWhenPulling", true,
                "If true, only collect while the player is attached and pulling the cart.");

            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"{PluginName} {PluginVersion} loaded.");
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }

    public class CartCollector : MonoBehaviour
    {
        private Vagon m_vagon;
        private Container m_container;
        private float m_timer;

        private void Awake()
        {
            m_vagon = GetComponent<Vagon>();
            m_container = GetComponentInChildren<Container>();
        }

        private void Update()
        {
            if (!CartAutoCollectPlugin.ModEnabled.Value) return;
            if (m_vagon == null || m_container == null) return;

            m_timer += Time.deltaTime;
            if (m_timer < CartAutoCollectPlugin.CollectionInterval.Value) return;
            m_timer = 0f;

            // If set to only collect while being pulled, check attachment
            if (CartAutoCollectPlugin.CollectWhenPulling.Value)
            {
                if (!m_vagon.IsAttached(Player.m_localPlayer))
                    return;
            }

            Inventory cartInventory = m_container.GetInventory();
            if (cartInventory == null) return;

            // Build a set of item names already in the cart
            HashSet<string> cartItemNames = new HashSet<string>();
            if (CartAutoCollectPlugin.OnlyMatchingItems.Value)
            {
                foreach (ItemDrop.ItemData item in cartInventory.GetAllItems())
                {
                    cartItemNames.Add(item.m_shared.m_name);
                }
                // Nothing in cart yet — nothing to match against
                if (cartItemNames.Count == 0) return;
            }

            // Find all ItemDrop objects within the radius
            float radius = CartAutoCollectPlugin.CollectionRadius.Value;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in colliders)
            {
                ItemDrop itemDrop = col.GetComponentInParent<ItemDrop>();
                if (itemDrop == null) continue;
                if (itemDrop.m_itemData == null) continue;

                // Skip items that can't be picked up yet (spawn delay)
                if (!itemDrop.CanPickup(true)) continue;

                string itemName = itemDrop.m_itemData.m_shared.m_name;

                // If OnlyMatchingItems is on, skip items not already in cart
                if (CartAutoCollectPlugin.OnlyMatchingItems.Value)
                {
                    if (!cartItemNames.Contains(itemName)) continue;
                }

                // Try to add the item to the cart inventory
                if (cartInventory.CanAddItem(itemDrop.m_itemData))
                {
                    cartInventory.AddItem(itemDrop.m_itemData);
                    ZNetScene.instance.Destroy(itemDrop.gameObject);
                }
            }
        }
    }

    [HarmonyPatch(typeof(Vagon), "Awake")]
    public static class VagonAwakePatch
    {
        static void Postfix(Vagon __instance)
        {
            if (__instance.GetComponent<CartCollector>() == null)
            {
                __instance.gameObject.AddComponent<CartCollector>();
            }
        }
    }
}
