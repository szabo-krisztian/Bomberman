public class MyVeryIntelligentZombieController : MyIntelligentZombieController
{
    protected override void RandomTickChangeDirection()
    {
        ChangeDirection(GetDirectionToNearestPath());
    }
}
