namespace MiamiOps
{
    public class SkillsTree
    {
        Round _roundContext;

        public SkillsTree(Round RoundContext)
        {
            _roundContext = RoundContext;
        }

        public void Update()
        {
            if (_roundContext.Player.LevelUp())
            {
                _roundContext.Player.Life += 10;
                _roundContext.Player.Speed += 0.005f;
            }
        }
    }
}
