using UnityEngine;

namespace DefaultNamespace.AirBaseScripts
{
    public class AircraftData
    {
        public Sprite aircraftSprite;
        public AircraftType aircraftType;
        private float maxHP;
        public float HP;
        private float maxFuel;
        public float fuel;
        
        public AircraftData(Sprite aircraftSprite, AircraftType aircraftType, float maxHp, float hp)
        {
            this.aircraftSprite = aircraftSprite;
            this.aircraftType = aircraftType;
            this.maxHP = maxHp;
            this.HP = hp;
        }

        public void RecoverAircraft(float hpRecoverySpeed,float fuelRecoverySpeed)
        {
            if (HP < maxHP)
            {
                HP += hpRecoverySpeed * Time.deltaTime;
            }

            if (fuel < maxFuel)
            {
                fuel += fuelRecoverySpeed * Time.deltaTime;
            }
        }
    }

    public enum AircraftType
    {
        Fighter,
        Bomber
    }
}