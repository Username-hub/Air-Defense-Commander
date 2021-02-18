using System;
using DefaultNamespace.Path;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyAircraftScript : AircraftScript
    {
        public TextMeshProUGUI debugText;
        public Transform enemyAim;
        public GameObject enemyTail;
 
        private void Start()
        {
            pathHandlerBase = GetComponent<PathHandlerBase>();
            Debug.Log(pathHandlerBase as EnemyPathHandler);
            (pathHandlerBase as EnemyPathHandler).BuildPath(enemyAim.position);
        }

        private void Update()
        {
            //TODO: Delete
            //debugText.text = "Enemy aim: " + enemyAim.name + "\n" + "Step to aim: " + (pathHandlerBase as EnemyPathHandler).stepsToAim.ToString();
            MoveForward(pathHandlerBase.getNextPoint());
        }
        
    }
} 