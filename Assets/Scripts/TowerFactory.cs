using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;
    Queue<Tower> towerQueue = new Queue<Tower>();
    
    public void AddTower(Waypoint baseWaypoint) {
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit) {
            InstantiateNewTower(baseWaypoint);
        }
        else {
            MoveExistingTower(baseWaypoint);
        }
        
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint) {
        var OldTower = towerQueue.Dequeue();
        OldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        newBaseWaypoint.transform.position = new Vector3(newBaseWaypoint.transform.position.x, newBaseWaypoint.transform.position.y+10, newBaseWaypoint.transform.position.z);
        OldTower.baseWaypoint = newBaseWaypoint;
        OldTower.transform.position = newBaseWaypoint.transform.position;
        towerQueue.Enqueue(OldTower);
     
    }

    private void InstantiateNewTower(Waypoint baseWaypoint) {
        
        Vector3 location = new Vector3(baseWaypoint.transform.position.x, baseWaypoint.transform.position.y+10, baseWaypoint.transform.position.z);
        var newTower = Instantiate(towerPrefab, location, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;
        towerQueue.Enqueue(newTower);
        
    }
}
