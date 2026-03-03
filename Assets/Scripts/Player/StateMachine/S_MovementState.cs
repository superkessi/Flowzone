public abstract class S_MovementState
{
    protected S_Player player;

    public enum StateType
    {
        NONE,
        IDLE,
        MOVE,
        JUMP,
        GLIDE,
        DIVE,
        SLOW,
        HIT,   
        BOOST,
        DEAD
    }

    protected S_MovementState(S_Player player)
    {
        this.player = player;
    }

    public abstract StateType Update();
    public abstract void FixedUpdate();
    public abstract void Enter();
    public abstract void Exit();
}