using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    public class PatrulhaState : IState
    {
        private List<Vector3> waypoints;
        private int currentWaypoint;
        private Agente self;
        public void OnEnter()
        {
            currentWaypoint = GetClosestWaypoint();
            self.Mover(waypoints[currentWaypoint]);
            self.ComeçarAEscutar();
        }
        
        public void OnUpdate()
        {
            if (!(Vector3.Distance(self.transform.position, waypoints[currentWaypoint]) < 0.6f)) return;
            currentWaypoint++;
            currentWaypoint %= waypoints.Count;
            self.Mover(waypoints[currentWaypoint]);

        }
        public void OnExit()
        {
            self.PararDeEscutar();
        }

        public PatrulhaState(Agente self, List<Vector3> waypoints)
        {
            this.waypoints = waypoints;
            this.self = self;
        }
        private int GetClosestWaypoint()
        {
            int closest = 0;
            float closestDistance = Vector3.Distance(self.transform.position, waypoints[0]);
            for (int i = 1; i < waypoints.Count; i++)
            {
                float distance = Vector3.Distance(self.transform.position, waypoints[i]);
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