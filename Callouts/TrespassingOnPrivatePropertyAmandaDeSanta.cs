using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using System.Drawing;
using System.Windows.Forms;

namespace OfficerMorrisonsGTAVStoryCharacterCalloutPack.Callouts
{

    [CalloutInterface("Trespassing On Private Property - Amanda DeSanta", CalloutProbability.Medium, "Code 2", "LSPD")]

    public class TrespassingOnPrivatePropertyAmandaDeSanta : Callout
    {


        // General Variables //
        private Ped Suspect;
        private Blip SuspectBlip;
        private Vector3 Spawnpoint;
        private float heading;
        private string malefemale;
        private int counter;

        public override bool OnBeforeCalloutDisplayed()
        {
            Spawnpoint = new Vector3();
            heading = 12.85f;
            ShowCalloutAreaBlipBeforeAccepting(Spawnpoint, 2500f);
            AddMinimumDistanceCheck(2500f, Spawnpoint);
            CalloutMessage = "An individual is trespassing on private property";
            CalloutPosition = Spawnpoint;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            Suspect = new Ped("IG_AMANDATOWNLEY", Spawnpoint, heading);
            Suspect.IsPersistent = true;
            Suspect.BlockPermanentEvents = true;
            CalloutInterfaceAPI.Functions.SendMessage(this, "A homeowner reported an individual trespassing on their property.");

            SuspectBlip = Suspect.AttachBlip();
            SuspectBlip.Color = System.Drawing.Color.White;
            SuspectBlip.IsRouteEnabled = true;

            if (Suspect.IsMale)
                malefemale = "sir";
            else
                malefemale = "ma'am";

            counter = 0;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if(Game.LocalPlayer.Character.DistanceTo(Suspect) <= 10f)
            {

                Game.DisplayHelp("Press 'Y' to interact with suspect.");

                if (Game.IsKeyDown(System.Windows.Forms.Keys.Y))
                {
                    counter++;

                    if(counter == 1)
                    {

                    }
                }
            }
        }


    }
}
