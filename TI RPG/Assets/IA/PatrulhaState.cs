using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    public class PatrulhaState : IState
    {
        private List<Vector3> waypoints;
        private int currentWaypoint;
        private Transform self;
        public void OnEnter()
        {
            currentWaypoint = GetClosestWaypoint();
            Debug.Log("Entrou Patrulha");
        }
        
        public void OnUpdate()
        {
            Debug.Log("Patrulhando");
            
            

        }
        public void OnExit()
        {
            Debug.Log("Saiu Patrulha");
        }
        public PatrulhaState(List<Vector3> waypoints, Transform self)
        {
            this.waypoints = waypoints;
            this.self = self;
        }
        private int GetClosestWaypoint()
        {
            int closest = 0;
            float closestDistance = Vector3.Distance(self.position, waypoints[0]);
            for (int i = 1; i < waypoints.Count; i++)
            {
                float distance = Vector3.Distance(self.position, waypoints[i]);
                if (distance < closestDistance)
                {
                    closest = i;
                    closestDistance = distance;
                }
            }
            return closest;
        }
        
    }

}