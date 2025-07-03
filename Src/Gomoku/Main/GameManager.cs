// ***********************************************************
// Type: Gomoku.Portable.GameManager
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

#nullable disable
namespace Gomoku.Portable
{
  public partial class GameManager
  {
    private Move lastUserMove;
    private Move lastAIMove;
    private GameManager.Turns myTurns = GameManager.Turns.FIRST_USER;
    private bool myByTurns;
    private GomokuAI myGomokuAI;
    private GameManager.GamingMode myGamingMode;
    private GameManager.GameState myGomokuGameState;

    internal GameManager()
    {
    }

    internal GameManager.Turns GetWhoTurns() => this.myTurns;

    internal void SetWhoTurns(GameManager.Turns turns) => this.myTurns = turns;

    internal GameManager.GameState GetGameState() => this.myGomokuGameState;

    internal int[][] GetTable() => this.myGomokuAI.t;

    internal bool IsUres(int a, int b) => this.myGomokuAI.IsUres(a, b);

    internal void NewGame(
      GameManager.GamingMode gameingMode,
      GameManager.StartingPlayer startingPlayer,
      int X,
      int Y,
      int aiStrength,
      int[][] autoLoadTable = null,
      bool autoLoad = true)
    {
      this.myGamingMode = gameingMode;
      this.lastAIMove = (Move) null;
      this.lastUserMove = (Move) null;
      this.myGomokuAI = new GomokuAI(X, Y, aiStrength);
      if (autoLoad && autoLoadTable != null)
      {
        this.myGomokuAI.t = autoLoadTable.Clone() as int[][];
        autoLoadTable = (int[][]) null;
      }
      else
        this.myGomokuAI.tabla_inicializalasa();
      this.lastAIMove = (Move) null;
      this.lastUserMove = (Move) null;
      if (this.myGamingMode == GameManager.GamingMode.AgainstComputer)
      {
        switch (startingPlayer)
        {
          case GameManager.StartingPlayer.Me:
            this.myTurns = GameManager.Turns.USER;
            break;
          case GameManager.StartingPlayer.Computer:
            this.myTurns = GameManager.Turns.AI;
            break;
          case GameManager.StartingPlayer.ByTurns:
            this.myTurns = !this.myByTurns ? GameManager.Turns.USER : GameManager.Turns.AI;
            this.myByTurns = !this.myByTurns;
            break;
        }
      }
      else if (this.myGamingMode == GameManager.GamingMode.TwoPlayer)
      {
        switch (startingPlayer)
        {
          case GameManager.StartingPlayer.Me:
            this.myTurns = GameManager.Turns.FIRST_USER;
            break;
          case GameManager.StartingPlayer.Computer:
            this.myTurns = GameManager.Turns.SECOND_USER;
            break;
          default:
            this.myTurns = !this.myByTurns ? GameManager.Turns.FIRST_USER : GameManager.Turns.SECOND_USER;
            this.myByTurns = !this.myByTurns;
            break;
        }
      }
      this.myGomokuGameState = GameManager.GameState.ACTIVE;
    }

    internal GameManager.MoveState GetAIMove(out Move move, GomokuAI.Felismert felismert)
    {
      int gamingMode = (int) this.myGamingMode;
      int x;
      int y;
      GomokuAI.State state = this.myGomokuAI.lepj(out x, out y, felismert);
      move = new Move(x, y);
      this.myTurns = GameManager.Turns.USER;
      switch (state)
      {
        case GomokuAI.State.VALID:
          this.lastAIMove = new Move(x, y);
          return GameManager.MoveState.VALID;
        case GomokuAI.State.AI_WIN:
          this.myGomokuGameState = GameManager.GameState.UNDEFINED;
          this.lastAIMove = (Move) null;
          this.lastUserMove = (Move) null;
          return GameManager.MoveState.AI_WIN;
        case GomokuAI.State.NO_WIN:
          this.lastAIMove = (Move) null;
          this.lastUserMove = (Move) null;
          this.myGomokuGameState = GameManager.GameState.UNDEFINED;
          return GameManager.MoveState.NO_WIN;
        default:
          return GameManager.MoveState.INVALID;
      }
    }

    internal GameManager.MoveState SetNextMove(Move move, GomokuAI.Felismert felismert)
    {
      int jel = 1;
      if (this.myTurns == GameManager.Turns.SECOND_USER)
        jel = 2;
      switch (this.myGomokuAI.rakhate(move.X, move.Y, jel, felismert))
      {
        case GomokuAI.State.INVALID:
          return GameManager.MoveState.INVALID;
        case GomokuAI.State.VALID:
          switch (this.myGamingMode)
          {
            case GameManager.GamingMode.AgainstComputer:
              this.InverseTurns();
              break;
            case GameManager.GamingMode.TwoPlayer:
              this.InverseTurns();
              break;
          }
          this.lastUserMove = new Move(move.X, move.Y);
          return GameManager.MoveState.VALID;
        case GomokuAI.State.USER_WIN:
          this.myGomokuGameState = GameManager.GameState.UNDEFINED;
          this.InverseTurns();
          this.lastAIMove = (Move) null;
          this.lastUserMove = (Move) null;
          if (this.myGamingMode == GameManager.GamingMode.AgainstComputer)
            return GameManager.MoveState.USER_WIN;
          return this.myGamingMode == GameManager.GamingMode.TwoPlayer && this.GetWhoTurns() == GameManager.Turns.FIRST_USER ? GameManager.MoveState.SECOND_USER_WIN : GameManager.MoveState.FIRST_USER_WIN;
        case GomokuAI.State.AI_WIN:
        case GomokuAI.State.NO_WIN:
          this.myGomokuGameState = GameManager.GameState.UNDEFINED;
          this.lastAIMove = (Move) null;
          this.lastUserMove = (Move) null;
          return GameManager.MoveState.NO_WIN;
        default:
          return GameManager.MoveState.INVALID;
      }
    }

    internal void Undo(out Move undoAIMove, out Move undoUserMove)
    {
      undoAIMove = (Move) null;
      undoUserMove = (Move) null;
      if (this.lastAIMove != null)
      {
        this.myGomokuAI.ClearForUndo(this.lastAIMove.X, this.lastAIMove.Y);
        undoAIMove = new Move(this.lastAIMove.X, this.lastAIMove.Y);
      }
      if (this.lastUserMove != null)
      {
        this.myGomokuAI.ClearForUndo(this.lastUserMove.X, this.lastUserMove.Y);
        undoUserMove = new Move(this.lastUserMove.X, this.lastUserMove.Y);
        if (this.myGamingMode == GameManager.GamingMode.TwoPlayer)
          this.InverseTurns();
      }
      this.lastAIMove = (Move) null;
      this.lastUserMove = (Move) null;
    }

    internal void InverseTurns()
    {
      if (this.myTurns == GameManager.Turns.FIRST_USER)
        this.myTurns = GameManager.Turns.SECOND_USER;
      else if (this.myTurns == GameManager.Turns.SECOND_USER)
      {
        this.myTurns = GameManager.Turns.FIRST_USER;
      }
      else
      {
        if (this.myTurns != GameManager.Turns.USER)
          return;
        this.myTurns = GameManager.Turns.AI;
      }
    }

    internal enum TableSize
    {
      Small,
      Medium,
      Large,
    }

    internal enum GamingMode
    {
      AgainstComputer,
      TwoPlayer,
    }

    internal enum StartingPlayer
    {
      Me,
      Computer,
      ByTurns,
    }

    internal enum MoveState
    {
      INVALID,
      VALID,
      USER_WIN,
      AI_WIN,
      FIRST_USER_WIN,
      SECOND_USER_WIN,
      NO_WIN,
    }

    internal enum GameState
    {
      UNDEFINED,
      ACTIVE,
    }

    internal enum Turns
    {
      NOBODY,
      AI,
      USER,
      FIRST_USER,
      SECOND_USER,
      MATCH_ENDED,
    }
  }
}
