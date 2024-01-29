using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Global player preferences cache. Stores keybinds and other values that can be changed in the settings menu.
/// Values can be received using the get functions. Please see example below
/// <code>
/// PlayerPrefCache.GetKeybind("WALK_FORWARD", "w");
/// PlayerPrefCache.GetPercentage("POV", 70);
/// </code>
///
/// If something isn't clear, there is documentation provided at each function
/// </summary>
public static class PlayerPrefCache {
    private static Dictionary<string, string> Keybinds = new();
    private static Dictionary<string, int> Percentages = new();
    private static Dictionary<string, string> Strings = new();


    #region Keybinds
    /// <summary>
    /// Returns the keybind stored or changed by the user. If the keybind does not exist, 
    /// it will return the default value given as the second argument
    /// </summary>
    /// <param name="key">The key of the keybind. E.g: WALK_FORWARD</param>
    /// <param name="defaultKey">The default value if the key isn't stored or changed already.</param>
    /// <returns></returns>
    public static string GetKeybind(string key, string defaultKey) {
        if (!Keybinds.ContainsKey(key))
            Keybinds.Add(key, PlayerPrefs.GetString(key) == "" ? defaultKey : PlayerPrefs.GetString(key));

        return Keybinds[key];
    }

    /// <summary>
    /// Updates the value of a keybind. If the key does not exist it will automatically create it in the cache.
    /// </summary>
    /// <param name="key">The name of the keybind</param>
    /// <param name="value">The new value</param>
    public static void UpdateKeybind(string key, string value) {
        Keybinds[key] = value;
    }
    #endregion

    #region Percentages
    /// <summary>
    /// Returns a percentage stored or changed by the user. If the key does not exist, 
    /// it will return the default value given as the second argument
    /// </summary>
    /// <param name="key">The key of the percentage. E.g: POV</param>
    /// <param name="defaultKey">The default value if the key isn't stored or changed already.</param>
    /// <returns></returns>
    public static int GetPercentage(string key, int defaultValue) {
        if (!Percentages.ContainsKey(key))
            Percentages.Add(key, PlayerPrefs.GetInt(key) == 0 ? defaultValue : PlayerPrefs.GetInt(key));

        return Percentages[key];
    }

    /// <summary>
    /// Updates the value of a percentage. If the key does not exist it will automatically create it in the cache.
    /// </summary>
    /// <param name="key">The name of the keybind</param>
    /// <param name="value">The new value</param>
    public static void UpdatePercentage(string key, int value) {
        Percentages[key] = value;
    }

    #endregion

    #region Strings
    /// <summary>
    /// Returns the str stored or changed by the user. If the key does not exist, 
    /// it will return the default value given as the second argument
    /// </summary>
    /// <param name="key">The key of the str. E.g: TEXTURE_DETAILS</param>
    /// <param name="defaultKey">The default value if the key isn't stored or changed already.</param>
    /// <returns></returns>
    public static string GetString(string key, string defaultValue) {
        if (!Strings.ContainsKey(key))
            Strings.Add(key, PlayerPrefs.GetString(key) == "" ? defaultValue : PlayerPrefs.GetString(key));

        return Strings[key];
    }

    /// <summary>
    /// Updates the value of a string. If the key does not exist it will automatically create it in the cache.
    /// </summary>
    /// <param name="key">The name of the keybind</param>
    /// <param name="value">The new value</param>
    public static void UpdateString(string key, string value) {
        Strings[key] = value;
    }

    #endregion
}
