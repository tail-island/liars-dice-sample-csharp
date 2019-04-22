using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardHead
{
    // ゲーム
    public sealed class Game
    {
        // プレイヤー
        [JsonProperty("players")]
        public Player[] Players { get; set; }

        // 現在のプレイヤーのインデックス
        [JsonProperty("player_index")]
        public int PlayerIndex { get; set; }

        // 現在のプレイヤーのサイコロの目
        [JsonIgnore]
        public int[] Faces
        {
            get { return Players[PlayerIndex].Faces; }
        }

        // 他のプレイヤーのサイコロの数の合計
        [JsonIgnore]
        public int SecretDiceCount
        {
            get { return Players.Select(player => player.Faces.Count()).Sum() - Faces.Count(); }
        }

        // 嘘つきサイコロのルールで数えた、自分のサイコロの目の数
        [JsonIgnore]
        public Dictionary<int, int> FaceCounts
        {
            get { return Enumerable.Range(2, 5).ToDictionary(targetFace => targetFace, targetFace => Faces.Where(face => face == targetFace || face == 1).Count()); }
        }

        // 確率で導いた目と個数
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

        // 前のプレイヤー
        [JsonIgnore]
        public Player PreviousPlayer
        {
            get { return Players[(PlayerIndex + Players.Length - 1) % Players.Count()]; }
        }

        // 前のプレイヤーの宣言。ゲーム開始時はnullになるので注意してください。
        [JsonIgnore]
        public Bid PreviousBid
        {
            get { return PreviousPlayer.Actions.Count() > 0 ? PreviousPlayer.Actions.Last().Bid : null; }
        }

        // 行動が合法か確認します。
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
