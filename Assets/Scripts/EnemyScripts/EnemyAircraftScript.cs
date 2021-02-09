using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyAircraftScript : AircraftScript
    {
        public Transform enemyAim;
        

        private void Start()
        {
            pathHandlerBase = GetComponent<EnemyPathHandler>();
            (pathHandlerBase as EnemyPathHandler).BuildPath(enemyAim.position);
        }

        private void Update()
        {
            MoveForward(pathHandlerBase.getNextPoint());
        }
        
    }
} 