using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardHead
{
    public sealed class Game
    {
        [JsonProperty("players")]
        public Player[] Players { get; set; }

        [JsonProperty("player_index")]
        public int PlayerIndex { get; set; }

        [JsonIgnore]
        public int[] Faces
        {
            get { return Players[PlayerIndex].Faces; }
        }

        [JsonIgnore]
        public int SecretDiceCount
        {
            get { return Players.Select(player => player.Faces.Count()).Sum() - Faces.Count(); }
        }

        [JsonIgnore]
        public Dictionary<int, int> FaceCounts
        {
            get { return Enumerable.Range(2, 5).ToDictionary(targetFace => targetFace, targetFace => Faces.Where(face => face == targetFace || face == 1).Count()); }
        }

        [JsonIgnore]
        public Dictionary<int, int> EstimatedFaceCounts
        {
            get
            {
                var faceCounts      = FaceCounts;
                var secretDiceCount = SecretDiceCount;

                return Enumerable.Range(2, 5).ToDictionary(targetFace => targetFace, targetFace => (int)Math.Round(secretDiceCount / 3.0f) + faceCounts[targetFace]);
            }
        }

        [JsonIgnore]
        public Player PreviousPlayer
        {
            get { return Players[(PlayerIndex + Players.Length - 1) % Players.Count()]; }
        }

        [JsonIgnore]
        public Bid PreviousBid
        {
            get { return PreviousPlayer.Actions.Count() > 0 ? PreviousPlayer.Actions.Last().Bid : null; }
        }

        public bool IsLegalAction(Action action)
        {
            if (action.Bid != null)
            {
                var bid = action.Bid;

                if (bid.Face < 2 || bid.Face > 6)
                {
                    return false;
                }

                if (bid.MinCount < 1)
                {
                    return false;
                }

                if (bid.MinCount > 20)
                {
                    return false;
                }

                var previousBid = PreviousBid;

                if (previousBid != null)
                {
                    if (bid.Face <= previousBid.Face && bid.MinCount <= previousBid.MinCount)
                    {
                        return false;
                    }

                    if (bid.Face >  previousBid.Face && bid.MinCount <  previousBid.MinCount)
                    {
                        return false;
                    }
                }

                return true;
            }

            if (action.Challenge != null)
            {
                if (PreviousBid == null)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
