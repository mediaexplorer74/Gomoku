// ***********************************************************
// Type: Gomoku.Portable.GomokuAI
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

using System;

#nullable disable
namespace Gomoku.Portable
{
  public partial class GomokuAI
  {
    private Random rand = new Random();
    private int myAIStrength;
    private int maxprioritasVeryHard = 74;
    private int maxprioritasHard = 68;
    private int maxprioritasNormal = 50;
    private int maxprioritasEasy = 28;
    private int maxprioritasVeryEasy = 22;
    private int jatekterx = 11;
    internal int jatektery = 15;
    internal int[][] t = new int[60][];
    private int[] gepnyert = new int[6]{ 2, 2, 2, 2, 2, 3 };
    private int[] jatekosnyert = new int[6]
    {
      1,
      1,
      1,
      1,
      1,
      3
    };
    private int[][] prioritasVeryHard = new int[74][]
    {
      new int[7]{ 4, 2, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 0, 4, 2, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 2, 0, 3 },
      new int[7]{ 2, 0, 2, 4, 2, 0, 2 },
      new int[7]{ 0, 4, 1, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 1, 0, 3 },
      new int[7]{ 1, 0, 1, 4, 1, 0, 1 },
      new int[7]{ 0, 0, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 0, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 0, 0, 3 },
      new int[7]{ 0, 4, 2, 2, 0, 0, 3 },
      new int[7]{ 0, 0, 2, 4, 2, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 0, 0, 3 },
      new int[7]{ 0, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 4, 2, 2, 2, 0, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 0, 3, 5 },
      new int[7]{ 0, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 0, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 0, 3, 5 },
      new int[7]{ 0, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 0, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 0, 3, 5 },
      new int[7]{ 4, 2, 0, 2, 2, 3, 5 },
      new int[7]{ 2, 0, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 2, 2, 0, 2, 3, 5 },
      new int[7]{ 2, 2, 0, 2, 4, 3, 5 },
      new int[7]{ 2, 2, 2, 0, 4, 3, 5 },
      new int[7]{ 0, 0, 1, 4, 1, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 0, 0, 3 },
      new int[7]{ 0, 0, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 0, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 0, 0, 3 },
      new int[7]{ 0, 4, 1, 1, 0, 0, 3 },
      new int[7]{ 0, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 0, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 0, 3, 5 },
      new int[7]{ 0, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 0, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 0, 3, 5 },
      new int[7]{ 0, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 0, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 0, 3, 5 },
      new int[7]{ 4, 1, 0, 1, 1, 3, 5 },
      new int[7]{ 1, 0, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 0, 1, 3, 5 },
      new int[7]{ 1, 1, 0, 1, 4, 3, 5 },
      new int[7]{ 1, 1, 1, 0, 4, 3, 5 },
      new int[7]{ 0, 2, 4, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 4, 2, 0, 3 },
      new int[7]{ 0, 4, 2, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 2, 4, 0, 3 },
      new int[7]{ 4, 0, 2, 0, 2, 0, 2 },
      new int[7]{ 2, 2, 4, 0, 0, 3, 5 },
      new int[7]{ 2, 4, 0, 0, 0, 3, 5 },
      new int[7]{ 0, 1, 4, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 4, 1, 0, 3 },
      new int[7]{ 0, 4, 1, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 1, 4, 0, 3 },
      new int[7]{ 4, 0, 1, 0, 1, 0, 1 },
      new int[7]{ 1, 1, 4, 0, 0, 3, 5 },
      new int[7]{ 1, 4, 0, 0, 0, 3, 5 }
    };
    private int[][] prioritasHard = new int[68][]
    {
      new int[7]{ 4, 2, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 0, 4, 2, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 2, 0, 3 },
      new int[7]{ 2, 0, 2, 4, 2, 0, 2 },
      new int[7]{ 0, 4, 1, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 1, 0, 3 },
      new int[7]{ 1, 0, 1, 4, 1, 0, 1 },
      new int[7]{ 0, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 4, 2, 2, 2, 0, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 0, 3, 5 },
      new int[7]{ 0, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 0, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 0, 3, 5 },
      new int[7]{ 0, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 0, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 0, 3, 5 },
      new int[7]{ 4, 2, 0, 2, 2, 3, 5 },
      new int[7]{ 2, 0, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 2, 2, 0, 2, 3, 5 },
      new int[7]{ 2, 2, 0, 2, 4, 3, 5 },
      new int[7]{ 2, 2, 2, 0, 4, 3, 5 },
      new int[7]{ 0, 0, 1, 4, 1, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 0, 0, 3 },
      new int[7]{ 0, 0, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 0, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 0, 0, 3 },
      new int[7]{ 0, 4, 1, 1, 0, 0, 3 },
      new int[7]{ 0, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 0, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 0, 3, 5 },
      new int[7]{ 0, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 0, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 0, 3, 5 },
      new int[7]{ 0, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 0, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 0, 3, 5 },
      new int[7]{ 4, 1, 0, 1, 1, 3, 5 },
      new int[7]{ 1, 0, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 0, 1, 3, 5 },
      new int[7]{ 1, 1, 0, 1, 4, 3, 5 },
      new int[7]{ 1, 1, 1, 0, 4, 3, 5 },
      new int[7]{ 0, 2, 4, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 4, 2, 0, 3 },
      new int[7]{ 0, 4, 2, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 2, 4, 0, 3 },
      new int[7]{ 4, 0, 2, 0, 2, 0, 2 },
      new int[7]{ 2, 2, 4, 0, 0, 3, 5 },
      new int[7]{ 2, 4, 0, 0, 0, 3, 5 },
      new int[7]{ 0, 1, 4, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 4, 1, 0, 3 },
      new int[7]{ 0, 4, 1, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 1, 4, 0, 3 },
      new int[7]{ 4, 0, 1, 0, 1, 0, 1 },
      new int[7]{ 1, 1, 4, 0, 0, 3, 5 },
      new int[7]{ 1, 4, 0, 0, 0, 3, 5 }
    };
    private int[][] prioritasNormal = new int[50][]
    {
      new int[7]{ 4, 2, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 0, 4, 2, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 2, 0, 3 },
      new int[7]{ 2, 0, 2, 4, 2, 0, 2 },
      new int[7]{ 0, 4, 1, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 1, 0, 3 },
      new int[7]{ 1, 0, 1, 4, 1, 0, 1 },
      new int[7]{ 2, 2, 4, 2, 0, 3, 5 },
      new int[7]{ 4, 2, 0, 2, 2, 3, 5 },
      new int[7]{ 2, 0, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 2, 2, 0, 2, 3, 5 },
      new int[7]{ 2, 2, 0, 2, 4, 3, 5 },
      new int[7]{ 2, 2, 2, 0, 4, 3, 5 },
      new int[7]{ 0, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 0, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 0, 3, 5 },
      new int[7]{ 0, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 0, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 0, 3, 5 },
      new int[7]{ 0, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 0, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 0, 3, 5 },
      new int[7]{ 4, 1, 0, 1, 1, 3, 5 },
      new int[7]{ 1, 0, 1, 1, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 0, 1, 3, 5 },
      new int[7]{ 1, 1, 0, 1, 4, 3, 5 },
      new int[7]{ 1, 1, 1, 0, 4, 3, 5 },
      new int[7]{ 0, 2, 4, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 4, 2, 0, 3 },
      new int[7]{ 0, 4, 2, 0, 2, 0, 3 },
      new int[7]{ 0, 2, 0, 2, 4, 0, 3 },
      new int[7]{ 4, 0, 2, 0, 2, 0, 2 },
      new int[7]{ 0, 1, 4, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 4, 1, 0, 3 },
      new int[7]{ 0, 4, 1, 0, 1, 0, 3 },
      new int[7]{ 0, 1, 0, 1, 4, 0, 3 },
      new int[7]{ 4, 0, 1, 0, 1, 0, 1 }
    };
    private int[][] prioritasEasy = new int[28][]
    {
      new int[7]{ 4, 2, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 4, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 4, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 0, 4, 2, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 2, 0, 3 },
      new int[7]{ 2, 0, 2, 4, 2, 0, 2 },
      new int[7]{ 0, 4, 1, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 1, 0, 3 },
      new int[7]{ 1, 0, 1, 4, 1, 0, 1 },
      new int[7]{ 0, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 4, 2, 2, 2, 0, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 0, 3, 5 },
      new int[7]{ 0, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 0, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 0, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 0, 3, 5 },
      new int[7]{ 0, 1, 1, 1, 4, 3, 5 }
    };
    private int[][] prioritasVeryEasy = new int[22][]
    {
      new int[7]{ 4, 2, 2, 2, 2, 3, 5 },
      new int[7]{ 2, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 1, 3, 5 },
      new int[7]{ 1, 1, 1, 1, 4, 3, 5 },
      new int[7]{ 0, 4, 2, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 2, 4, 0, 3 },
      new int[7]{ 0, 2, 4, 2, 2, 0, 3 },
      new int[7]{ 0, 2, 2, 4, 2, 0, 3 },
      new int[7]{ 0, 4, 1, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 1, 4, 0, 3 },
      new int[7]{ 0, 1, 4, 1, 1, 0, 3 },
      new int[7]{ 0, 1, 1, 4, 1, 0, 3 },
      new int[7]{ 0, 4, 2, 2, 2, 3, 5 },
      new int[7]{ 4, 2, 2, 2, 0, 3, 5 },
      new int[7]{ 2, 2, 2, 4, 0, 3, 5 },
      new int[7]{ 0, 2, 2, 2, 4, 3, 5 },
      new int[7]{ 0, 4, 1, 1, 1, 3, 5 },
      new int[7]{ 4, 1, 1, 1, 0, 3, 5 },
      new int[7]{ 1, 1, 1, 4, 0, 3, 5 },
      new int[7]{ 0, 1, 1, 1, 4, 3, 5 }
    };

    public GomokuAI(int x, int y, int aiStrength)
    {
      this.jatekterx = x;
      this.jatektery = y;
      this.myAIStrength = aiStrength;
    }

    private GomokuAI.tpoz felismer(int[] x, GomokuAI.Felismert felismert)
    {
      GomokuAI.tpoz[] tpozArray = new GomokuAI.tpoz[1600];
      for (int index = 0; index < 1600; ++index)
        tpozArray[index] = new GomokuAI.tpoz();
      int index1 = 0;
      for (int index2 = 0; index2 < this.jatekterx; ++index2)
      {
        for (int index3 = 0; index3 < this.jatektery; ++index3)
        {
          int num1 = 0;
          int num2 = 0;
          for (int index4 = 0; index4 < 7; ++index4)
          {
            switch (x[index4])
            {
              case 3:
                num2 += 7 - index4;
                index4 = 7;
                break;
              case 4:
                if (this.t[index2 + index4][index3] == 0)
                  ++num2;
                num1 = index4;
                break;
              default:
                if (this.t[index2 + index4][index3] == x[index4])
                {
                  ++num2;
                  break;
                }
                break;
            }
          }
          if (num2 == 7)
          {
            tpozArray[index1].pozicio[0] = index2 + num1;
            tpozArray[index1].pozicio[1] = index3;
            ++index1;
            felismert.StartX = index2;
            felismert.StartY = index3;
            felismert.Direction = 0;
          }
        }
      }
      for (int index5 = 0; index5 < this.jatekterx; ++index5)
      {
        for (int index6 = 0; index6 < this.jatektery; ++index6)
        {
          int num3 = 0;
          int num4 = 0;
          for (int index7 = 0; index7 < 7; ++index7)
          {
            switch (x[index7])
            {
              case 3:
                num4 += 7 - index7;
                index7 = 7;
                break;
              case 4:
                if (this.t[index5][index6 + index7] == 0)
                  ++num4;
                num3 = index7;
                break;
              default:
                if (this.t[index5][index6 + index7] == x[index7])
                {
                  ++num4;
                  break;
                }
                break;
            }
          }
          if (num4 == 7)
          {
            tpozArray[index1].pozicio[0] = index5;
            tpozArray[index1].pozicio[1] = index6 + num3;
            ++index1;
            felismert.StartX = index5;
            felismert.StartY = index6;
            felismert.Direction = 1;
          }
        }
      }
      for (int index8 = 0; index8 < this.jatekterx; ++index8)
      {
        for (int index9 = 0; index9 < this.jatektery; ++index9)
        {
          int num5 = 0;
          int num6 = 0;
          for (int index10 = 0; index10 < 7; ++index10)
          {
            switch (x[index10])
            {
              case 3:
                num6 += 7 - index10;
                index10 = 7;
                break;
              case 4:
                if (this.t[index8 + index10][index9 + index10] == 0)
                  ++num6;
                num5 = index10;
                break;
              default:
                if (this.t[index8 + index10][index9 + index10] == x[index10])
                {
                  ++num6;
                  break;
                }
                break;
            }
          }
          if (num6 == 7)
          {
            tpozArray[index1].pozicio[0] = index8 + num5;
            tpozArray[index1].pozicio[1] = index9 + num5;
            ++index1;
            felismert.StartX = index8;
            felismert.StartY = index9;
            felismert.Direction = 2;
          }
        }
      }
      for (int index11 = 4; index11 < this.jatekterx; ++index11)
      {
        for (int index12 = 0; index12 < this.jatektery; ++index12)
        {
          int num7 = 0;
          int num8 = 0;
          for (int index13 = 0; index13 < 7; ++index13)
          {
            if (index11 - index13 < 0)
            {
              if (x[index13] == 3 && num8 == index13)
              {
                num8 = 7;
                break;
              }
              break;
            }
            switch (x[index13])
            {
              case 3:
                num8 += 7 - index13;
                index13 = 7;
                break;
              case 4:
                if (this.t[index11 - index13][index12 + index13] == 0)
                  ++num8;
                num7 = index13;
                break;
              default:
                if (this.t[index11 - index13][index12 + index13] == x[index13])
                {
                  ++num8;
                  break;
                }
                break;
            }
          }
          if (num8 == 7)
          {
            tpozArray[index1].pozicio[0] = index11 - num7;
            tpozArray[index1].pozicio[1] = index12 + num7;
            ++index1;
            felismert.StartX = index11;
            felismert.StartY = index12;
            felismert.Direction = 3;
          }
        }
      }
      GomokuAI.tpoz tpoz = new GomokuAI.tpoz();
      if (index1 == 0)
      {
        tpoz.pozicio[0] = 161;
        return tpoz;
      }
      int index14 = this.rand.Next() % index1;
      tpoz.pozicio[0] = tpozArray[index14].pozicio[0];
      tpoz.pozicio[1] = tpozArray[index14].pozicio[1];
      return tpoz;
    }

    internal void tabla_inicializalasa()
    {
      for (int index1 = 0; index1 < this.jatekterx; ++index1)
      {
        this.t[index1] = new int[this.jatektery];
        for (int index2 = 0; index2 < this.jatektery; ++index2)
          this.t[index1][index2] = 0;
      }
      for (int jatekterx = this.jatekterx; jatekterx < this.jatekterx + 7; ++jatekterx)
      {
        this.t[jatekterx] = new int[this.jatektery + 7];
        for (int index = 0; index < this.jatektery + 7; ++index)
          this.t[jatekterx][index] = 161;
      }
      for (int index = 0; index < this.jatekterx; ++index)
      {
        this.t[index] = new int[this.jatektery + 7];
        for (int jatektery = this.jatektery; jatektery < this.jatektery + 7; ++jatektery)
          this.t[index][jatektery] = 161;
      }
    }

    internal int tabla_teli()
    {
      int num = 161;
      for (int index1 = 0; index1 < this.jatekterx; ++index1)
      {
        for (int index2 = 0; index2 < this.jatektery; ++index2)
        {
          if (this.t[index1][index2] == 0)
            num = 0;
        }
      }
      return num;
    }

    internal GomokuAI.State lepj(out int x, out int y, GomokuAI.Felismert felismert)
    {
      x = -1;
      y = -1;
      int num1 = 0;
      if (this.myAIStrength == 4)
      {
        for (int index = 0; index < this.maxprioritasVeryHard; ++index)
        {
          GomokuAI.tpoz tpoz = this.felismer(this.prioritasVeryHard[index], felismert);
          if (tpoz.pozicio[0] != 161)
          {
            this.t[tpoz.pozicio[0]][tpoz.pozicio[1]] = 2;
            num1 = 1;
            x = tpoz.pozicio[0];
            y = tpoz.pozicio[1];
            break;
          }
        }
      }
      else if (this.myAIStrength == 3)
      {
        for (int index = 0; index < this.maxprioritasHard; ++index)
        {
          GomokuAI.tpoz tpoz = this.felismer(this.prioritasHard[index], felismert);
          if (tpoz.pozicio[0] != 161)
          {
            this.t[tpoz.pozicio[0]][tpoz.pozicio[1]] = 2;
            num1 = 1;
            x = tpoz.pozicio[0];
            y = tpoz.pozicio[1];
            break;
          }
        }
      }
      else if (this.myAIStrength == 2)
      {
        for (int index = 0; index < this.maxprioritasNormal; ++index)
        {
          GomokuAI.tpoz tpoz = this.felismer(this.prioritasNormal[index], felismert);
          if (tpoz.pozicio[0] != 161)
          {
            this.t[tpoz.pozicio[0]][tpoz.pozicio[1]] = 2;
            num1 = 1;
            x = tpoz.pozicio[0];
            y = tpoz.pozicio[1];
            break;
          }
        }
      }
      else if (this.myAIStrength == 1)
      {
        for (int index = 0; index < this.maxprioritasEasy; ++index)
        {
          GomokuAI.tpoz tpoz = this.felismer(this.prioritasEasy[index], felismert);
          if (tpoz.pozicio[0] != 161 && this.rand.Next() % 8 == 1)
          {
            this.t[tpoz.pozicio[0]][tpoz.pozicio[1]] = 2;
            num1 = 1;
            x = tpoz.pozicio[0];
            y = tpoz.pozicio[1];
            break;
          }
        }
      }
      else if (this.myAIStrength == 0)
      {
        for (int index = 0; index < this.maxprioritasVeryEasy; ++index)
        {
          GomokuAI.tpoz tpoz = this.felismer(this.prioritasVeryEasy[index], felismert);
          if (tpoz.pozicio[0] != 161 && this.rand.Next() % 100 == 1)
          {
            this.t[tpoz.pozicio[0]][tpoz.pozicio[1]] = 2;
            num1 = 1;
            x = tpoz.pozicio[0];
            y = tpoz.pozicio[1];
            break;
          }
        }
      }
      if (num1 == 0)
      {
        int num2 = 0;
        int index1;
        int index2;
        do
        {
          if (num2 < 20)
          {
            index1 = 5 + this.rand.Next() % 3;
            index2 = 4 + this.rand.Next() % 3;
          }
          else if (num2 < 100)
          {
            index1 = 5 + this.rand.Next() % 7;
            index2 = 4 + this.rand.Next() % 7;
          }
          else
          {
            index1 = this.rand.Next() % this.jatekterx;
            index2 = this.rand.Next() % this.jatektery;
          }
          ++num2;
        }
        while (this.t[index1][index2] != 0);
        this.t[index1][index2] = 2;
        x = index1;
        y = index2;
      }
      if (this.felismer(this.gepnyert, felismert).pozicio[0] != 161)
        return GomokuAI.State.AI_WIN;
      return this.tabla_teli() == 161 ? GomokuAI.State.NO_WIN : GomokuAI.State.VALID;
    }

    internal bool IsUres(int a, int b) => this.t[a][b] == 0;

    internal void ClearForUndo(int a, int b) => this.t[a][b] = 0;

    internal GomokuAI.State rakhate(int a, int b, int jel, GomokuAI.Felismert felismert)
    {
      if (a >= this.jatekterx || b >= this.jatektery || this.t[a][b] != 0)
        return GomokuAI.State.INVALID;
      this.t[a][b] = jel;
      if ((jel != 1 ? this.felismer(this.gepnyert, felismert) : this.felismer(this.jatekosnyert, felismert)).pozicio[0] != 161)
        return GomokuAI.State.USER_WIN;
      return this.tabla_teli() == 161 ? GomokuAI.State.NO_WIN : GomokuAI.State.VALID;
    }

    internal class tpoz
    {
      internal GomokuAI.pozicioClass pozicio = new GomokuAI.pozicioClass();
    }

    internal class pozicioClass
    {
      private int[] myData = new int[2];

      internal int this[int pos]
      {
        get => this.myData[pos];
        set => this.myData[pos] = value;
      }
    }

    public class Felismert
    {
      public int StartX;
      public int StartY;
      public int Direction;
    }

    internal enum State
    {
      INVALID,
      VALID,
      USER_WIN,
      AI_WIN,
      NO_WIN,
    }
  }
}
