using TechFix.Common.Helper;

namespace TechFix.Common.Enums
{
    public enum UserTitle
    {
        [TitleInfo("AGENT", 0, 0, 0, 0, 0, 0)] Agent = 1,

        [TitleInfo("SUPERVISOR", 25000, 4000, 30, 3, 1500, 100)]
        Supervisor = 2,

        [TitleInfo("MANAGER", 50000, 6000, 20, 5, 2500, 500)]
        Manager = 3,

        [TitleInfo("REGIONAL MANAGER", 100000, 10000, 10, 7, 4000, 2000)]
        RegionalManager = 4,

        [TitleInfo("STATE MANAGER", 200000, 15000, 10, 9, 6000, 10000)]
        StateManager = 5,

        [TitleInfo("DIRECTOR", 600000, 20000, 10, 12, 8000, 50000)]
        Director = 6,

        [TitleInfo("GLOBAL DIRECTOR", 1800000, 25000, 10, 15, 10000, 100000)]
        GlobalDirector = 7,

        [TitleInfo("CHAIRMAN", 5400000, 30000, 10, 18, 15000, 250000)]
        Chairman = 8
    }
}

