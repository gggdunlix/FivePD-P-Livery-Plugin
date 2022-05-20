using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json.Schema;

namespace forceextras
{
    public class forceextras
    {
        public class Plugin : FivePD.API.Plugin
        {
            internal Plugin()
            {

                EventHandlers["FivePD::Client::SpawnVehicle"] += new Action<int, int>(EnableExtras); //Natixco

            }


            private void EnableExtras(int playerServerID, int vehicleNetworkID)
            {


                Vehicle pvehicle = Game.PlayerPed.CurrentVehicle;
                var vehiclename = Game.PlayerPed.CurrentVehicle.DisplayName.ToLower();
                var config = API.LoadResourceFile(API.GetCurrentResourceName(), "/config/vehicles.json");
                var json = JObject.Parse(config);
                JToken policeVehicles = json["police"];
                Debug.Write("\nVehicle Name: " + vehiclename);

                if (policeVehicles.FirstOrDefault(vehicle => vehicle["vehicle"].ToString().ToLower() == vehiclename)["livery"] != null)
                {
                    JToken playerVehicleConfig = policeVehicles.FirstOrDefault(vehicle => vehicle["vehicle"].ToString().ToLower() == vehiclename);
                    JToken livery = playerVehicleConfig["livery"];
                    Debug.Write("\n Livery:" + livery);
                    API.SetVehicleLivery(pvehicle.Handle, ((int)livery));
                    Debug.Write("\nLivery " + livery + " set on vehicle " + vehiclename + ".");
                    
                }
                else
                {
                    Debug.Write("\n" + vehiclename + " has no livery defined vehicles.json. Check vMenu for available liveries.");

                }
                return;
            }
        }
    }
}
