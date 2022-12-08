using System;
using UnityEngine;

public enum BallType
{
    wooden,
    stone
}

public interface IBallParameters
{
	public BallType Type { get; set; }
	public float MoveForce { get; set; }
    public float JumpForce { get; set; }
    public float Mass { get; set; }

    public void OnApply();
}

public class WoodenBallParameters : IBallParameters
{
    private BallType type = BallType.wooden;
    private float moveForce = 5;
    private float jumpForce = 2;
    private float mass = 1;

    public BallType Type { get => type; set => type = value; }
    public float MoveForce { get => moveForce; set => moveForce = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float Mass { get => mass; set => mass = value; }
    

    public void OnApply()
	{
		Debug.Log("WoodenBall parametes applied");
	}
}

public class StoneBallParameters : IBallParameters
{
    private BallType type = BallType.stone;
    private float moveForce = 7;
    private float jumpForce = 0;
    private float mass = 3;
    public BallType Type { get => type; set => type = value; }
    public float MoveForce { get => moveForce; set => moveForce = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float Mass { get => mass; set => mass = value; }

    public void OnApply()
	{
		Debug.Log("StoneBall parametes applied");
	}
}
