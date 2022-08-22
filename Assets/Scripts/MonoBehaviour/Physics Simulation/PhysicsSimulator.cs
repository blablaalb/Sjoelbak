using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using MEC;

public class PhysicsSimulator : Singleton<PhysicsSimulator>
{
    [SerializeField]
    private PuckGhost _puckGhostPrefab;
    [SerializeField, Range(0, 5000)]
    private int _simulationSteps = 500;
    private TrajectoryRenderer _trajectoryRenderer;

    override protected void Awake()
    {
        base.Awake();
        Physics.autoSimulation = false;
        MakeBoardGhost();
        _trajectoryRenderer = GetComponent<TrajectoryRenderer>();
    }

    internal void FixedUpdate()
    {
        GetMainPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    /// <summary>
    /// Obstaons a ghost scene. Crates a new one if none is crated.
    /// </summary>
    /// <returns></returns>
    public Scene GetGhostScene()
    {
        return GhostSceneManager.Instance.GetGhostScene();
    }

    public Scene GetMainScene()
    {
        return MainSceneManager.Instance.GetMainScene();
    }

    public PhysicsScene GetGhostPhysicsScene()
    {
        return GhostSceneManager.Instance.GetGhostPhysicsScene();
    }

    public PhysicsScene GetMainPhysicsScene()
    {
        return MainSceneManager.Instance.GetMainPhysicsScene();
    }

    private Board MakeBoardGhost()
    {
        return GhostSceneManager.Instance.GetBoardGhost();
    }

    /// <summary>
    /// Simulates the physics with the given force.
    /// </summary>
    /// <param name="force">Force to simmulate.</param>
    public void Simulate(Vector3 force, ForceMode forceMode)
    {
        try
        {
            PuckGhost puckGhost = PuckGhostManager.Instance.GetPuckGhost();
            puckGhost.Stop();
            puckGhost.SetPosition(RealPuckManager.Instance.GetRealPuck().Position);
            puckGhost.AddForce(force, forceMode);
            DrawTrajectoryLine(puckGhost);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }
    }

    private void DrawTrajectoryLine(PuckGhost puckGhost)
    {
        Vector3[] points = new Vector3[_simulationSteps];
        for (int i = 0; i < _simulationSteps; i++)
        {
            GetGhostPhysicsScene().Simulate(Time.fixedDeltaTime);
            Vector3 point = puckGhost.Position;
            // Offsetting the point a little above the board so the board becomes visible.
            point.y += 0.1f;
            points[i] = point;
        }
        _trajectoryRenderer.RenderPoints(points);
    }
}