namespace MiamiOps
{
    public class SkillsTree
    {
        Round _roundContext;

        int _skillPoints;
        
        public SkillsTree(Round RoundContext)
        {
            _roundContext = RoundContext;

            _skillPoints = 0;
        }

        public void UpdateSkillPoints()
        {
            _skillPoints = _roundContext.Player.Level / 5;
        }

        public void Update()
        {
            if (_roundContext.Player.LevelUp()) UpdateSkillPoints();
        }

        public int SkillPoints => _skillPoints;
    }
}
