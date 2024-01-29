using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefCache {
    private static Dictionary<string, string> Keybinds = new();

    public static string GetKeybind(string key, string defaultKey) {
        if (!Keybinds.ContainsKey(key))
            Keybinds.Add(key, PlayerPrefs.GetString(key) == "" ? defaultKey : PlayerPrefs.GetString(key));

        Keybinds.TryGetValue(key, out string val);
        return val;
    }

    public static void UpdateKeybind(string key, string value) {
        Keybinds[key] = value;
    }
}
