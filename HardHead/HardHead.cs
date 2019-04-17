using Newtonsoft.Json;
using System;
using System.Linq;

namespace HardHead
{
    public class HardHead
    {
        public void CheckOtherPrograms(Career[] careers)
        {
            ;
        }

        public Action Action(Game game)
        {
            // ログ出力。標準出力は通信で使うので、ログ出力したい場合はConsole.Error.WriteLineを使用してください。
            Console.Error.WriteLine("Action() started.");

            // 前のプレイヤーの宣言。
            var previousBid = game.PreviousBid;

            // 確率から推測された、各目の数。
            var estimatedFaceCounts = game.EstimatedFaceCounts;

            // もし前のプレイヤーが宣言をしているなら……
            if (previousBid != null)
            {
                // もし確率よりも大きな数を宣言していたなら……
                if (previousBid.MinCount > estimatedFaceCounts[previousBid.Face])
                {
                    // チャレンジ！
                    return new Action { Challenge = new Challenge() };
                }
            }

            // 宣言候補を作成します。
            var actionCandidates = Enumerable.Range(2, 5).                                                           // ダイスの目である2～6を、
                Select(face => new Action { Bid = new Bid { Face = face, MinCount = estimatedFaceCounts[face] } }).  // 確率が示す数で宣言して、
                Where(action => game.IsLegalAction(action)).                                                         // ルールが許す宣言だけ残して、
                ToArray();                                                                                           // 配列にします。

            // 宣言候補がないなら……
            if (actionCandidates.Count() == 0)
            {
                // しょうがないのでチャレンジします。
                return new Action { Challenge = new Challenge() };
            }

            // 宣言候補からランダムで一つ選択します。
            return actionCandidates[new Random().Next(actionCandidates.Count())];
        }

        //  public void GameEnd(Game game)
        //  {
        //      ;
        //  }

        public void Execute()
        {
            var terminated = false;

            while (!terminated)
            {
                var commandString = Console.ReadLine();
                var parameterString = Console.ReadLine();

                switch (commandString)
                {
                    case "check_other_programs":
                        CheckOtherPrograms(JsonConvert.DeserializeObject<Career[]>(parameterString));
                        Console.WriteLine("OK");

                        break;

                    case "action":
                        var resultString = JsonConvert.SerializeObject(Action(JsonConvert.DeserializeObject<Game>(parameterString)));
                        Console.WriteLine(resultString);

                        break;

                    case "game_end":
                        // GameEnd(JsonConvert.DeserializeObject<Game>(parameterString));
                        Console.WriteLine("OK");

                        break;

                    default:
                        terminated = true;
                        break;
                }

            }
        }

    }
}
