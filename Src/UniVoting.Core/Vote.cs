using System;

namespace Univoting.Core
{
    /// <summary>
    /// A class which represents the Vote table.
    /// </summary>
    public partial class Vote
    {
        public  int Id { get; set; }
        public  int VoterId { get; set; }
        public  int CandidateId { get; set; }
        public  int PositionId { get; set; }
        public  DateTime Time { get; set; }
        public  Voter Voter { get; set; }
        public  Candidate Candidate { get; set; }
        public  Position Position { get; set; }
        //public  PollingStation PollingStation { get; set; }
        //public  string PollingStationId { get; set; }
    }
}