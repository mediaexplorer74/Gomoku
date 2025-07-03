// ***********************************************************
// Type: Gomoku.Settings
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

using Gomoku.Portable;

#nullable disable
namespace Gomoku
{
  internal partial class Settings : SettingsBase
  {
    private static readonly string GAME_MODE = nameof (GAME_MODE);
    private static readonly string STARTING_PLAYER = nameof (STARTING_PLAYER);
    private static readonly string TABLE_SIZE = nameof (TABLE_SIZE);
    private static readonly string SELECTED_IMAGE_INDEX = nameof (SELECTED_IMAGE_INDEX);
    private static readonly string SELECTED_IMAGE_ISX = nameof (SELECTED_IMAGE_ISX);
    private static readonly string FIRST_PLAYER_NAME = nameof (FIRST_PLAYER_NAME);
    private static readonly string SECOND_PLAYER_NAME = nameof (SECOND_PLAYER_NAME);
    private static readonly string SOUND_DISABLED = nameof (SOUND_DISABLED);
    private static readonly string BACKUP_DISABLED = nameof (BACKUP_DISABLED);
    private static readonly string AI_STRENGTH = nameof (AI_STRENGTH);
    private static readonly string SCORE_WIN = nameof (SCORE_WIN);
    private static readonly string SCORE_LOST = nameof (SCORE_LOST);
    private static readonly string SCORE_DAWN = nameof (SCORE_DAWN);
    private static readonly string TABLE = nameof (TABLE);
    private static readonly string TURNS = nameof (TURNS);
    private static readonly string START_COUNT = nameof (START_COUNT);
    private static readonly string IS_RATED = nameof (IS_RATED);

    internal int scoreWin
    {
      get => this.TryGetValue<int>(Settings.SCORE_WIN);
      set => this.AddOrUpdateValue(Settings.SCORE_WIN, (object) value);
    }

    internal int scoreLost
    {
      get => this.TryGetValue<int>(Settings.SCORE_LOST);
      set => this.AddOrUpdateValue(Settings.SCORE_LOST, (object) value);
    }

    internal int scoreDown
    {
      get => this.TryGetValue<int>(Settings.SCORE_DAWN);
      set => this.AddOrUpdateValue(Settings.SCORE_DAWN, (object) value);
    }

    internal int[][] Table
    {
      get => this.TryGetValue<int[][]>(Settings.TABLE);
      set => this.AddOrUpdateValue(Settings.TABLE, (object) value);
    }

    internal GameManager.Turns Turns
    {
      get => this.TryGetValue<GameManager.Turns>(Settings.TURNS);
      set => this.AddOrUpdateValue(Settings.TURNS, (object) value);
    }

    internal string firstPlayerName
    {
      get => this.TryGetValue<string>(Settings.FIRST_PLAYER_NAME) ?? "";
      set => this.AddOrUpdateValue(Settings.FIRST_PLAYER_NAME, (object) value);
    }

    internal string secondPlayerName
    {
      get => this.TryGetValue<string>(Settings.SECOND_PLAYER_NAME) ?? "";
      set => this.AddOrUpdateValue(Settings.SECOND_PLAYER_NAME, (object) value);
    }

    internal int selectedImageIndex
    {
      get
      {
        int num = this.TryGetValue<int>(Settings.SELECTED_IMAGE_INDEX);
        return num <= 0 ? MainPage.GetDefaultSkinImageIndex() : num;
      }
      set => this.AddOrUpdateValue(Settings.SELECTED_IMAGE_INDEX, (object) value);
    }

    internal bool selectedImageIsX
    {
      get => this.TryGetValue<bool>(Settings.SELECTED_IMAGE_ISX);
      set => this.AddOrUpdateValue(Settings.SELECTED_IMAGE_ISX, (object) value);
    }

    internal GameManager.GamingMode gamingMode
    {
      get => this.TryGetValue<GameManager.GamingMode>(Settings.GAME_MODE);
      set => this.AddOrUpdateValue(Settings.GAME_MODE, (object) value);
    }

    internal GameManager.StartingPlayer startingPlayer
    {
      get => this.TryGetValue<GameManager.StartingPlayer>(Settings.STARTING_PLAYER);
      set => this.AddOrUpdateValue(Settings.STARTING_PLAYER, (object) value);
    }

    internal bool soundDisabled
    {
      get => this.TryGetValue<bool>(Settings.SOUND_DISABLED);
      set => this.AddOrUpdateValue(Settings.SOUND_DISABLED, (object) value);
    }

    internal bool backupDisabled
    {
      get => this.TryGetValue<bool>(Settings.BACKUP_DISABLED);
      set => this.AddOrUpdateValue(Settings.BACKUP_DISABLED, (object) value);
    }

    internal int AIStrength
    {
      get => this.TryGetValue<int>(Settings.AI_STRENGTH);
      set => this.AddOrUpdateValue(Settings.AI_STRENGTH, (object) value);
    }

    internal GameManager.TableSize tableSize
    {
      get => this.TryGetValue<GameManager.TableSize>(Settings.TABLE_SIZE);
      set => this.AddOrUpdateValue(Settings.TABLE_SIZE, (object) value);
    }

    internal int X
    {
      get
      {
        switch (this.tableSize)
        {
          case GameManager.TableSize.Small:
            return 19;
          case GameManager.TableSize.Medium:
            return 30;
          case GameManager.TableSize.Large:
            return 50;
          default:
            return 19;
        }
      }
    }

    internal int Y
    {
      get
      {
        switch (this.tableSize)
        {
          case GameManager.TableSize.Small:
            return 19;
          case GameManager.TableSize.Medium:
            return 30;
          case GameManager.TableSize.Large:
            return 50;
          default:
            return 19;
        }
      }
    }

    internal int StartCount
    {
      get => this.TryGetValue<int>(Settings.START_COUNT);
      set => this.AddOrUpdateValue(Settings.START_COUNT, (object) value);
    }

    internal bool IsRated
    {
      get => this.TryGetValue<bool>(Settings.IS_RATED);
      set => this.AddOrUpdateValue(Settings.IS_RATED, (object) value);
    }
  }
}
