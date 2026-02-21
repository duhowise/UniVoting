using System.Collections.Concurrent;
using System.Collections.Generic;
using UniVoting.Model;

namespace UniVoting.Services;

public interface IClientSessionService
{
    Voter? CurrentVoter { get; set; }
    Stack<Position> Positions { get; set; }
    ConcurrentBag<Vote> Votes { get; set; }
    ConcurrentBag<SkippedVotes> SkippedVotes { get; set; }
    Position? CurrentPosition { get; set; }
    Candidate? CurrentCandidate { get; set; }
}
