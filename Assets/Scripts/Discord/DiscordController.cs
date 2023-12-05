using System;
using Discord;
using UnityEngine;

public class DiscordController : MonoBehaviour {
    private static long _time;
    private static bool _loggedIn;
    private static Discord.Discord _discord;
    private static readonly string[] CHAPTER_NAME = { "Memories, Awakening", "Not Alone", "Escape... & Despair" };
    private static ActivityManager _activityManager;

    public static void ClearActivity() {
        _loggedIn = false;
        _activityManager.ClearActivity((res) => {
            if(res != Result.Ok) Debug.LogWarning("Failed to clear activity");
        });
    }

    private static void UpdateRPC() {
        if(!_loggedIn) return;
        try {
            _activityManager = _discord.GetActivityManager();
            var activity = new Activity {
                Details = GameState.level == -1
                              ? ""
                              : "Chapter " + (GameState.level + 1) + ": " + CHAPTER_NAME[GameState.level],
                State = GameState.level == -1 ? "In the Main Menu" : "Playing",
                Assets = {
                    LargeImage = "mm_logo",
                    LargeText = "Playing Murder Mansion",
                },
                Timestamps = {
                    Start = _time
                }
            };

            _activityManager.UpdateActivity(activity, (res) => {
                if(res != Result.Ok) Debug.LogWarning("Failed to update activity");
            });
            Debug.Log("Set Activity");
        } catch {
            Debug.LogWarning("Unexpected error occurred while trying to update discord activity");
        }
    }

    private void Update() {
        try {
            _discord.RunCallbacks();
            UpdateRPC();
        } catch {
            Debug.LogWarning("Failed to connect to discord");
        }
    }

    private void Start() {
        if(_loggedIn) return;

        if (GameState.secrets == null) {
            Debug.LogError("Secrets file not assigned. Discord RPC will not work");
            return;
        }

        _time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _discord = new Discord.Discord(GameState.secrets.appId, (ulong)CreateFlags.NoRequireDiscord);
        _loggedIn = true;
    }
}
