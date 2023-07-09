using Rage;
using System;
using System.Collections.Generic;
using System.Collections;
using LSPD_First_Response.Mod.API;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace OfficerMorrisonsGTAVStoryCharacterCalloutPack
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;
            Game.LogTrivial("Plugin OfficerMorrison's GTA V Story Character Callout Pack" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "by OfficerMorrison has been initialized!");
            Game.LogTrivial("Go on duty to fully load OfficerMorrison's GTA V Story Character Callout Pack");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LSPDFRResolveEventHandler);
        }

        public override void Finally()
        {
            Game.LogTrivial("OfficerMorrison's GTA V Story Character Callout Pack has successfully cleaned up!");
        }

        private static void OnOnDutyStateChangedHandler(bool OnDuty)
        {
            if (OnDuty)
            {
                RegisterCallouts();

                Game.DisplayNotification("Officer Morrison's GTA V Story Character Callout Pack by OfficerMorrison | ~y~Version 0.1.0 | has successfully loaded!");
            }
        }

        private static void RegisterCallouts()
        {
            Functions.RegisterCallout(typeof(Callouts.IllegalCampfireOnPublicBeacAmandaDeSanta));
        }

        public static Assembly LSPDFRResolveEventHandler(object sender, ResolveEventArgs args)
        {
            foreach (Assembly assembly in Functions.GetAllUserPlugins())
            {
                if (args.Name.ToLower().Contains(assembly.GetName().Name.ToLower()))
                {
                    return assembly;
                }
            }
            return null;
        }

        public static bool IsLSPDFRPluginRunning(string Plugin, Version minversion = null)
        {
            foreach (Assembly assembly in Functions.GetAllUserPlugins())
            {
                AssemblyName an = assembly.GetName();
                if (an.Name.ToLower() == Plugin.ToLower())
                {
                    if (minversion == null || an.Version.CompareTo(minversion) >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}