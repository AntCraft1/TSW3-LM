﻿#nullable disable warnings
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Animation;

namespace TSW3LM
{
    static class Config
    {
        private static Entries entries = null;

        private static string Path;

        internal static void Init(string path)
        {
            Path = path;

            if (File.Exists(Path))
            {
                Log.AddLogMessage("Loading Config...", "Config:<init>", Log.LogLevel.DEBUG);
                entries = JsonConvert.DeserializeObject<Entries>(File.ReadAllText(path));
                Log.AddLogMessage("Config loaded", "Config:<init>", Log.LogLevel.DEBUG);
            }
            else { 
                Log.AddLogMessage("No Config found, initializing empty config", "Config:<init>", Log.LogLevel.DEBUG);
                entries = new Entries();
            }
        }

        public static bool SkipAutosave
        {
            set { entries.SkipAutosave = value; Save(); }
        }

        public static string GamePath
        {
            get { return entries.GamePath; }
            set { entries.GamePath = value; if (!entries.SkipAutosave) Save(); }
        }

        public static string LibraryPath
        {
            get { return entries.LibraryPath; }
            set { entries.LibraryPath = value; if (!entries.SkipAutosave) Save(); }
        }
        public static bool NoUpdate
        {
            get { return entries.NoUpdate; }
            set { entries.NoUpdate = value; if (!entries.SkipAutosave) Save(); }
        }

        public static bool DevUpdates
        {
            get { return entries.DevUpdates; }
            set { entries.DevUpdates = value; if (!entries.SkipAutosave) Save(); }
        }

        public static int MaxGameLiveries
        {
            get { return entries.MaxGameLiveries; }
            set { entries.MaxGameLiveries = value; if (!entries.SkipAutosave) Save(); }
        }

        public static bool CollectLiveryData
        {
            get { return entries.CollectLiveryData; }
            set { entries.CollectLiveryData = value; if (!entries.SkipAutosave) Save(); }
        }

        private static void Save()
        {
            Log.AddLogMessage("Saving Config...", "Config::Save", Log.LogLevel.DEBUG);
            File.WriteAllText(Path, JsonConvert.SerializeObject(entries));
            Log.AddLogMessage("Config saved", "Config::Save", Log.LogLevel.DEBUG);
        }

        internal static void ApplyDefaults()
        {
            entries.ApplyDefaults();
            Save();
        }

        private class Entries
        {
            [JsonIgnore]
            internal bool SkipAutosave = false;
            internal string GamePath = "";
            internal string LibraryPath = "";
            internal bool NoUpdate = false;
            internal bool DevUpdates = false;
            internal int MaxGameLiveries = 300;
            internal bool CollectLiveryData = true;

            internal void ApplyDefaults()
            {
                SkipAutosave = false;
                GamePath = "";
                LibraryPath = "";
                NoUpdate = false;
                DevUpdates = false;
                MaxGameLiveries = 300;
                CollectLiveryData = true;
            }
        }

    }

}
