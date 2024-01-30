using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public static class KeyListener {
    private static Dictionary<string, List<Action<string>>> Callbacks = new();

    static KeyListener() {
        InputSystem.onAnyButtonPress.Call(input => {
            if (!Callbacks.ContainsKey(input.name)) return;
            Callbacks[input.name].ForEach(a => a.Invoke(input.name));
        });
    }

    public static void AddCallback(string _key, Action<string> callback) {
        string key = _key.ToLower();
        if (!Callbacks.ContainsKey(key)) Callbacks[key] = new List<Action<string>>();
        Callbacks[key].Add(callback);
    }
}
