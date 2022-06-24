using Ardalis.SmartEnum;

namespace Mgame.Core.Domain.Enums;

/// <summary>
/// Variants of teams allowed in the game
/// </summary>
public abstract class TeamType : SmartEnum<TeamType>
{
    public static TeamType Single = new TeamTypeSingle();
    public static TeamType Double = new TeamTypeDouble();

    private TeamType(int value, string name, int membersCount) : base(name, value)
    {
        MembersCount = membersCount;
    }

    public int MembersCount { get; }

    sealed class TeamTypeSingle : TeamType
    {
        public TeamTypeSingle() : base(0, "single", 1) { }
    }

    sealed class TeamTypeDouble : TeamType
    {
        public TeamTypeDouble() : base(1, "double", 2) { }
    }
}
