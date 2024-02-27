using System;
using System.Text.RegularExpressions;

namespace practice_task_10
{
    class Program
    {
        static void Main()
        {
            Regex chessCoordReg = new Regex(@"[a-h][1-8]");
            Random randchar = new Random();
            string[] chessChars = new string[] { "a", "b", "c", "d", "e", "f", "g", "h"};

            Console.Write("введите размер доски, согласно шахматной записи (до h8 включительно): ");
            string boardDimentions = Console.ReadLine().ToLower();

            string blackPiecePosition = chessChars[randchar.Next(0, 8)] + randchar.Next(1, 9).ToString();
            Console.WriteLine("Позиция чёрной фигуры: " + blackPiecePosition);

            string whitePiecePosition = chessChars[randchar.Next(0, 8)] + randchar.Next(1, 9).ToString();
            Console.WriteLine("Позиция белой фигуры: " + whitePiecePosition);

            Console.WriteLine("Введите тип белой фигуры: ");
            Console.WriteLine("1 - лодья; 2 - слон; 3 - ферзь; 4 - король;");
            int whitePieceType = Convert.ToInt32(Console.ReadLine());

            string[] strings = new string[3] { boardDimentions, blackPiecePosition, whitePiecePosition };

            if (!ValidFormat(chessCoordReg, strings))
            {
                Console.WriteLine("Введены данные, неправильные по формату!");
                return;
            }

            blackPiecePosition = ConvertChessCoordsToNormalCoords(blackPiecePosition);
            whitePiecePosition = ConvertChessCoordsToNormalCoords(whitePiecePosition);
            boardDimentions = ConvertChessCoordsToNormalCoords(boardDimentions);

            strings = new string[3] { boardDimentions, blackPiecePosition, whitePiecePosition };

            for (int i = 1; i < 3; i++)
            {
                if (CoordsNotValid(strings[i], boardDimentions))
                {
                    Console.WriteLine("Введеы некорректные данные");
                    return;
                }
            }
            
            if (CheckMove(whitePiecePosition, blackPiecePosition, whitePieceType)) 
            {
                if (whitePieceType == 4) { Console.WriteLine("Король может дойти до поля."); }
                else { Console.WriteLine("Фигура не угражает полю."); }
            }
            else { Console.WriteLine("Фигура МОЖЕТ атаковать поле."); }

        }

        static bool ValidFormat(Regex strFormat, string[] strToCheck)
        {
            for (int i = 0; i < strToCheck.Length; i++)
            {
                if (!strFormat.IsMatch(strToCheck[i])) return false;
            }
            return true;
        }

        static string ConvertChessCoordsToNormalCoords(string chessCoords)
        {
            int xCoord = chessCoords[0] - 'a';
            int yCoord = int.Parse(chessCoords[1].ToString()) - 1;

            return new string(xCoord.ToString() + yCoord.ToString());
        }

        static Boolean CoordsNotValid(string piecePosition, string boardDimentions)
        {
            int pieceX = piecePosition[0] - '0';
            int pieceY = piecePosition[1] - '0';
            int boardX = boardDimentions[0] - '0';
            int boardY = boardDimentions[1] - '0';

            if ((pieceX > boardX || pieceY > boardY) ||
                (pieceX < 0 || pieceY < 0))
            { return true; }

            return false;
        }

        static bool CheckMove(string piecePosition, string piceDestination, int pieceType)
        {
            int pieceX = Convert.ToInt32(piecePosition[0] - '0');
            int pieceY = Convert.ToInt32(piecePosition[1] - '0');
            int DestX = Convert.ToInt32(piceDestination[0] - '0');
            int DestY = Convert.ToInt32(piceDestination[1] - '0');

            switch (pieceType)
            {
                case 1: return !(pieceX == DestX || pieceY == DestY);
                case 2: return !(Math.Abs(pieceX - DestX) == Math.Abs(pieceY - DestY));
                case 3:
                    return !((Math.Abs(pieceX - DestX) == Math.Abs(pieceY - DestY))
                        || (pieceX == DestX || pieceY == DestY));
                case 4: return ((Math.Abs(pieceX - DestX) <= 1 && Math.Abs(pieceY - DestY) == 1)
                        || (Math.Abs(pieceX - DestX) == 1 && Math.Abs(pieceY - DestY) <= 1));
            }
            return true;
        }
    }
}