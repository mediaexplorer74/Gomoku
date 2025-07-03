// ***********************************************************
// Type: Gomoku.Resources.AppResources
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

using System.Globalization;

#nullable disable
namespace Gomoku.Resources
{
  public partial class AppResources
  {
    private static ResourceManagerFactory ResourceManager = new ResourceManagerFactory();
    private static CultureInfo resourceCulture = (CultureInfo) null;

    public static string About
    {
      get => AppResources.ResourceManager.GetString(nameof (About), AppResources.resourceCulture);
    }

    public static string About2
    {
      get => AppResources.ResourceManager.GetString("About", AppResources.resourceCulture);
    }

    public static string AgainstComputer
    {
      get
      {
        return AppResources.ResourceManager.GetString("Against Computer", AppResources.resourceCulture);
      }
    }

    public static string AIEasy
    {
      get => AppResources.ResourceManager.GetString("Easy", AppResources.resourceCulture);
    }

    public static string AIHard
    {
      get => AppResources.ResourceManager.GetString("Hard", AppResources.resourceCulture);
    }

    public static string AINormal
    {
      get => AppResources.ResourceManager.GetString("Normal", AppResources.resourceCulture);
    }

    public static string AIStrength
    {
      get
      {
        return AppResources.ResourceManager.GetString("Artificial Intelligence strength", AppResources.resourceCulture);
      }
    }

    public static string AIVeryEasy
    {
      get => AppResources.ResourceManager.GetString("VeryEasy", AppResources.resourceCulture);
    }

    public static string AIVeryHard
    {
      get => AppResources.ResourceManager.GetString("VeryHard", AppResources.resourceCulture);
    }

    public static string Apply
    {
      get => AppResources.ResourceManager.GetString(nameof (Apply), AppResources.resourceCulture);
    }

    public static string BuyFullVersion
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (BuyFullVersion), AppResources.resourceCulture);
      }
    }

    public static string BuyFullVersionCaption
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (BuyFullVersionCaption), AppResources.resourceCulture);
      }
    }

    public static string BuyFullVersionShould
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (BuyFullVersionShould), AppResources.resourceCulture);
      }
    }

    public static string BuyFullVersionWillExpire
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (BuyFullVersionWillExpire), AppResources.resourceCulture);
      }
    }

    public static string ByTurns
    {
      get => AppResources.ResourceManager.GetString("By Turns", AppResources.resourceCulture);
    }

    public static string Cancel
    {
      get => AppResources.ResourceManager.GetString(nameof (Cancel), AppResources.resourceCulture);
    }

    public static string Computer
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (Computer), AppResources.resourceCulture);
      }
    }

    public static string CopyRight
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (CopyRight), AppResources.resourceCulture);
      }
    }

    public static string Draw
    {
      get => AppResources.ResourceManager.GetString(nameof (Draw), AppResources.resourceCulture);
    }

    public static string EmailMe
    {
      get => AppResources.ResourceManager.GetString(nameof (EmailMe), AppResources.resourceCulture);
    }

    public static string FirstPlayer
    {
      get => AppResources.ResourceManager.GetString("First Player", AppResources.resourceCulture);
    }

    public static string FirstPlayerName
    {
      get
      {
        return AppResources.ResourceManager.GetString("First Player Name", AppResources.resourceCulture);
      }
    }

    public static string GameMode
    {
      get => AppResources.ResourceManager.GetString("Game Mode", AppResources.resourceCulture);
    }

    public static string General
    {
      get => AppResources.ResourceManager.GetString(nameof (General), AppResources.resourceCulture);
    }

    public static string Gomoku
    {
      get => AppResources.ResourceManager.GetString(nameof (Gomoku), AppResources.resourceCulture);
    }

    public static string GomokuInfo
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (GomokuInfo), AppResources.resourceCulture);
      }
    }

    public static string InvalidPosition
    {
      get
      {
        return AppResources.ResourceManager.GetString("Invalid position!", AppResources.resourceCulture);
      }
    }

    public static string JustSendMessage
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (JustSendMessage), AppResources.resourceCulture);
      }
    }

    public static string Large
    {
      get => AppResources.ResourceManager.GetString(nameof (Large), AppResources.resourceCulture);
    }

    public static string Me
    {
      get => AppResources.ResourceManager.GetString(nameof (Me), AppResources.resourceCulture);
    }

    public static string Medium
    {
      get => AppResources.ResourceManager.GetString(nameof (Medium), AppResources.resourceCulture);
    }

    public static string NewGame
    {
      get => AppResources.ResourceManager.GetString(nameof (NewGame), AppResources.resourceCulture);
    }

    public static string PlayersName
    {
      get => AppResources.ResourceManager.GetString("Players Name", AppResources.resourceCulture);
    }

    public static string PleaseEmailMe
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (PleaseEmailMe), AppResources.resourceCulture);
      }
    }

    public static string PleaseReview
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (PleaseReview), AppResources.resourceCulture);
      }
    }

    public static string RateAndReview
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (RateAndReview), AppResources.resourceCulture);
      }
    }

    public static string RemoveAdds
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (RemoveAdds), AppResources.resourceCulture);
      }
    }

    public static string Review
    {
      get => AppResources.ResourceManager.GetString(nameof (Review), AppResources.resourceCulture);
    }

    public static string SecondPlayer
    {
      get => AppResources.ResourceManager.GetString("Second Player", AppResources.resourceCulture);
    }

    public static string SecondPlayerName
    {
      get => AppResources.ResourceManager.GetString("2. Player", AppResources.resourceCulture);
    }

    public static string SendMe
    {
      get => AppResources.ResourceManager.GetString(nameof (SendMe), AppResources.resourceCulture);
    }

    public static string SendMessageDoNotSay
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (SendMessageDoNotSay), AppResources.resourceCulture);
      }
    }

    public static string SendMessageEmptyText
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (SendMessageEmptyText), AppResources.resourceCulture);
      }
    }

    public static string SendMessageThankYou
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (SendMessageThankYou), AppResources.resourceCulture);
      }
    }

    public static string Settings
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (Settings), AppResources.resourceCulture);
      }
    }

    public static string Skin
    {
      get => AppResources.ResourceManager.GetString(nameof (Skin), AppResources.resourceCulture);
    }

    public static string Small
    {
      get => AppResources.ResourceManager.GetString(nameof (Small), AppResources.resourceCulture);
    }

    public static string Sound
    {
      get => AppResources.ResourceManager.GetString(nameof (Sound), AppResources.resourceCulture);
    }

    public static string StartingPlayer
    {
      get
      {
        return AppResources.ResourceManager.GetString("Starting Player", AppResources.resourceCulture);
      }
    }

    public static string Table
    {
      get => AppResources.ResourceManager.GetString(nameof (Table), AppResources.resourceCulture);
    }

    public static string TableSize
    {
      get => AppResources.ResourceManager.GetString("Table Size", AppResources.resourceCulture);
    }

    public static string TapImageToChangeSkin
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (TapImageToChangeSkin), AppResources.resourceCulture);
      }
    }

    public static string Turns
    {
      get => AppResources.ResourceManager.GetString(nameof (Turns), AppResources.resourceCulture);
    }

    public static string TwoPlayers
    {
      get
      {
        return AppResources.ResourceManager.GetString(nameof (TwoPlayers), AppResources.resourceCulture);
      }
    }

    public static string Undo
    {
      get => AppResources.ResourceManager.GetString(nameof (Undo), AppResources.resourceCulture);
    }

    public static string WinTheMatch
    {
      get
      {
        return AppResources.ResourceManager.GetString("wins the match!", AppResources.resourceCulture);
      }
    }

    public static string YouLostTheMatch
    {
      get
      {
        return AppResources.ResourceManager.GetString("You have lost the match!", AppResources.resourceCulture);
      }
    }

    public static string YouTurns
    {
      get => AppResources.ResourceManager.GetString("You Turn", AppResources.resourceCulture);
    }

    public static string YouWin
    {
      get => AppResources.ResourceManager.GetString("You Win!", AppResources.resourceCulture);
    }
  }
}
