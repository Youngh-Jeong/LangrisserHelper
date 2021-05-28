using System;
using System.Collections.Generic;
using System.Text;

namespace LangrisserHelper.Shared
{
    public class TurnAction
    {
        public bool isFirst = false;
        public bool isOpponent = false;
        public int maxChoice = 0;

        public bool IsActive(bool first, bool op) 
        {
            return ((isFirst == first) && (op == isOpponent));
        }
    }
    public static class StaticTurnData 
    {
        public static Dictionary<int, TurnAction> turnActions = new Dictionary<int, TurnAction>() // n번째에, 선/후공(isFrist)의 픽/밴(isOpponent)가 활성화
        {
            { 1, new TurnAction(){ isFirst=false, isOpponent =true, maxChoice=1 } },    // 후공이 1개 밴
            { 2, new TurnAction(){ isFirst=true, isOpponent =true, maxChoice=1 } },     // 선공이 1개 밴
            { 3, new TurnAction(){ isFirst=true, isOpponent =false, maxChoice=1 } },    // 선공이 1개 픽 (1개픽)
            { 4, new TurnAction(){ isFirst=false, isOpponent =true, maxChoice=2 } },    // 후공이 2개 밴
            { 5, new TurnAction(){ isFirst=false, isOpponent =false, maxChoice=1 } },   // 후공이 1개 픽 (1개픽)
            { 6, new TurnAction(){ isFirst=true, isOpponent =true, maxChoice=2 } },     // 선공이 2개 밴
            { 7, new TurnAction(){ isFirst=true, isOpponent =false, maxChoice=1 } },    // 선공이 1개 픽 (2개픽)
            { 8, new TurnAction(){ isFirst=false, isOpponent =true, maxChoice=2 } },    // 후공이 2개 밴
            { 9, new TurnAction(){ isFirst=false, isOpponent =false, maxChoice=1 } },   // 후공이 1개 픽 (2개픽)
            { 10, new TurnAction(){ isFirst=true, isOpponent =true, maxChoice=2 } },    // 선공이 2개 밴
            { 11, new TurnAction(){ isFirst=true, isOpponent =false, maxChoice=1 } },   // 선공이 1개 픽 (3개픽)
            { 12, new TurnAction(){ isFirst=false, isOpponent =true, maxChoice=2 } },   // 후공이 2개 밴
            { 13, new TurnAction(){ isFirst=false, isOpponent =false, maxChoice=1 } },  // 후공이 1개 픽 (3개 픽)
            { 14, new TurnAction(){ isFirst=true, isOpponent =true, maxChoice=2 } },    // 선공이 2개 밴
            { 15, new TurnAction(){ isFirst=true, isOpponent =false, maxChoice=1 } },   // 선공이 1개 픽 (4개픽)
            { 16, new TurnAction(){ isFirst=false, isOpponent =true, maxChoice=2 } },   // 후공이 2개 밴
            { 17, new TurnAction(){ isFirst=false, isOpponent =false, maxChoice=1 } },  // 후공이 1개 픽 (4개 픽)
            { 18, new TurnAction(){ isFirst=true, isOpponent =true, maxChoice=2 } },    // 선공이 2개 밴
            { 19, new TurnAction(){ isFirst=true, isOpponent =false, maxChoice=1 } },   // 선공이 1개 픽 (5개픽)
            { 20, new TurnAction(){ isFirst=false, isOpponent =false, maxChoice=1 } },  // 후공이 1개 픽 (5개 픽)
        };
    }
}
