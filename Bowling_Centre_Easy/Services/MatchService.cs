using Bowling_Centre_Easy.Entities;
using Bowling_Centre_Easy.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Centre_Easy.Services
{
    public class MatchService
    {
        // The repository for storing matches.
        private readonly MatchRepo _matchRepo;
        private readonly LaneRepo _laneRepo;

        // Constructor: We inject the MatchRepo so this service can store and retrieve matches.
        public MatchService(MatchRepo matchRepo, LaneRepo laneRepo)
        {
            _matchRepo = matchRepo;
            _laneRepo = laneRepo;
        }

        // Creates a new match with the given list of players.
        // This method initializes the match date, assigns the players,
        // and creates a new Scorecard instance that is associated with the match.
        public Match CreateMatch(List<Player> players, BowlingLane lane)
        {
            // Create a new scorecard. 
            Scorecard scorecard = new Scorecard();

            // Create a new match instance.

            Match match = new Match
            {
                Date = DateTime.Now,  
                Players = players,
                BowlingLane = lane,
                Scorecard = scorecard 
            };

            // Store the match in our in-memory repository.
            _matchRepo.Add(match);

            // Return the created match so it can be used by the calling code.
            return match;
        }

        // Retrieves a match by its unique ID.
        public Match GetMatchById(Guid matchId)
        {
            return _matchRepo.GetById(matchId);
        }

    }
}
