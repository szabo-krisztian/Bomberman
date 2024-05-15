public class MyVeryIntelligentZombieController : MyIntelligentZombieController
{
    /// <summary>
    /// The VeryIntelligentZombie is the same as the IntelligentZombie, however this method varies. This behaviour sets the zombie's direction to the shortest path to one player even when he receives a random signal.
    /// </summary>
    protected override void RandomTickChangeDirection()
    {
        ChangeDirection(GetDirectionToNearestPath());
    }
}
