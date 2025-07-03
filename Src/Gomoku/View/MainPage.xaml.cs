// ***********************************************************
// Type: Gomoku.MainPage
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Gomoku.Portable;
using Gomoku.Resources;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace Gomoku
{
    public partial class MainPage : Page
    {
        private static int myIconSize = 44;
        private double myZoomFactor = 1.0;
        private volatile bool isAIThinking;
        private volatile bool isPintch = default; // 
        private bool myMainPageUIIsInSettingsMode;
        internal static GameManager TheGameManager = new GameManager();
        internal static Settings mySettings = new Settings();
        private List<List<Image>> imageList = new List<List<Image>>();
        private static readonly int MaxNumberOfImages = 6;
        private static readonly string O = nameof(O);
        private static readonly string X = nameof(X);
        private static readonly string JPEG = ".jpg";
        private static Image prevUserImage = (Image)null;
        private static readonly string SkinImagePath = "ms-appx:/Images/Skins/";
        internal static readonly string SkinImagePrefix = MainPage.SkinImagePath + "image";
        //private LicenseInformation licenseInformation;


        //MainPage
        public MainPage()
        {
            this.InitializeComponent();


            if (MainPage.mySettings.StartCount == 0)
            {
                MainPage.mySettings.AIStrength = 3;
                MainPage.mySettings.tableSize = GameManager.TableSize.Large;
            }
            ++MainPage.mySettings.StartCount;

            //InterestialAdManager.Instance.RequestNextAds();

            bool flag = false;
            try
            {
                flag = MainPage.mySettings.Turns == GameManager.Turns.MATCH_ENDED;
            }
            catch
            {
            }
            this.StartNewGame(!flag);

            // Correctly attach the Loaded event handler using the += operator
            this.Loaded += MainPage_LoadedHook;
        }//MainPage


        // MainPage_LoadedHook
        private async void MainPage_LoadedHook(object sender, RoutedEventArgs e)
        {
            this.MainPage_Loaded(sender, e);
            if (MainPage.mySettings.IsRated || MainPage.mySettings.StartCount % 4 != 0)
                return;
            string str = "Gomoku";
            MessageDialog messageDialog = new MessageDialog(
                "Thank you for using " + str + "! It would be very helpful for us if you could rate and review our app! Would you like to rate and review it now?",
                "Rate and review " + str);
            messageDialog.Commands.Add((IUICommand)new UICommand("Yes", (UICommandInvokedHandler)null, (object)"YES"));
            messageDialog.Commands.Add((IUICommand)new UICommand("No", (UICommandInvokedHandler)null, (object)"NO"));
            messageDialog.Commands.Add((IUICommand)new UICommand("Do not remind me", (UICommandInvokedHandler)null, (object)"NEVER"));
            IUICommand iuiCommand = await messageDialog.ShowAsync();
            if ((string)iuiCommand.Id == "YES")
            {
                this.myReviewButton_Click((object)null, (RoutedEventArgs)null);
                MainPage.mySettings.IsRated = true;
            }
            else
            {
                if (!((string)iuiCommand.Id == "NEVER"))
                    return;
                MainPage.mySettings.IsRated = true;
            }
        }//MainPage_LoadedHook


        // MainPage_Loaded
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ContentPanelSrollViewer.ChangeView(null, this.ContentPanelSrollViewer.ScrollableHeight / 2.0, null);
            this.ContentPanelSrollViewer.ChangeView(this.ContentPanelSrollViewer.ScrollableWidth / 2.0, null, null);
        }//MainPage_Loaded


        // OnNavigatedTo
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
            }
            catch
            {
            }
        }

        private void HandleImageTap(object sender)
        {
            if (this.isAIThinking || this.isPintch 
                || MainPage.TheGameManager.GetGameState() != GameManager.GameState.ACTIVE 
                || !(sender is Image image) || !(((FrameworkElement)image).Tag is ImageTag tag))
                return;
            Move move = new Move(tag.xCoordinate, tag.yCoordinate);
            GomokuAI.Felismert felismert = new GomokuAI.Felismert();
            GameManager.MoveState gameState = MainPage.TheGameManager.SetNextMove(move, felismert);
            this.HandleGameState(gameState, felismert);
            switch (gameState)
            {
                case GameManager.MoveState.INVALID:
                    if (gameState != GameManager.MoveState.INVALID)
                        return;
                    MainPage.PlayInvalidSound();
                    return;
                case GameManager.MoveState.USER_WIN:
                    MainPage.DisplayFirstUserImage(image);
                    MainPage.DisplayFirstUserWin(this.imageList, felismert);
                    break;
                default:
                    if (MainPage.TheGameManager.GetWhoTurns() == GameManager.Turns.AI)
                    {
                        MainPage.DisplayFirstUserImage(image);
                        this.DoAIMove();
                        break;
                    }
                    if (MainPage.TheGameManager.GetWhoTurns() == GameManager.Turns.FIRST_USER && gameState != GameManager.MoveState.SECOND_USER_WIN)
                    {
                        MainPage.DisplaySecondUserImage(image);
                        break;
                    }
                    if (MainPage.TheGameManager.GetWhoTurns() == GameManager.Turns.SECOND_USER && gameState != GameManager.MoveState.FIRST_USER_WIN)
                    {
                        MainPage.DisplayFirstUserImage(image);
                        break;
                    }
                    break;
            }
            MainPage.PlayValidSound();
        }

        private void HandleGameState(GameManager.MoveState gameState, GomokuAI.Felismert f)
        {
            switch (gameState)
            {
                case GameManager.MoveState.INVALID:
                    this.ApplicationTitle.Text = AppResources.InvalidPosition;
                    break;
                case GameManager.MoveState.VALID:
                    this.DisplayWhoTurns();
                    this.myUndoButton.IsEnabled = true;
                    break;
                case GameManager.MoveState.USER_WIN:
                    this.ApplicationTitle.Text = AppResources.YouWin;
                    MainPage.DisplayFirstUserWin(this.imageList, f);
                    this.myUndoButton.IsEnabled = false;
                    break;
                case GameManager.MoveState.AI_WIN:
                    this.ApplicationTitle.Text = AppResources.YouLostTheMatch;
                    MainPage.DisplaySecondUserWin(this.imageList, f);
                    this.myUndoButton.IsEnabled = false;
                    break;
                case GameManager.MoveState.FIRST_USER_WIN:
                    MainPage.DisplayFirstUserWin(this.imageList, f);
                    if (string.IsNullOrEmpty(MainPage.mySettings.firstPlayerName))
                        this.ApplicationTitle.Text = AppResources.FirstPlayer + " " + AppResources.WinTheMatch;
                    else
                        this.ApplicationTitle.Text = MainPage.mySettings.firstPlayerName + " " + AppResources.WinTheMatch;
                    this.myUndoButton.IsEnabled = false;
                    break;
                case GameManager.MoveState.SECOND_USER_WIN:
                    MainPage.DisplaySecondUserWin(this.imageList, f);
                    if (string.IsNullOrEmpty(MainPage.mySettings.secondPlayerName))
                        this.ApplicationTitle.Text = AppResources.SecondPlayer + " " + AppResources.WinTheMatch;
                    else
                        this.ApplicationTitle.Text = MainPage.mySettings.secondPlayerName + " " + AppResources.WinTheMatch;
                    this.myUndoButton.IsEnabled = false;
                    break;
                case GameManager.MoveState.NO_WIN:
                    this.ApplicationTitle.Text = AppResources.Draw;
                    break;
            }
        }

        private void DisplayWhoTurns()
        {
            switch (MainPage.TheGameManager.GetWhoTurns())
            {
                case GameManager.Turns.USER:
                    this.ApplicationTitle.Text = AppResources.YouTurns;
                    break;
                case GameManager.Turns.FIRST_USER:
                    if (string.IsNullOrEmpty(MainPage.mySettings.firstPlayerName))
                    {
                        this.ApplicationTitle.Text = AppResources.FirstPlayer + " " + AppResources.Turns;
                        break;
                    }
                    this.ApplicationTitle.Text = MainPage.mySettings.firstPlayerName + " " + AppResources.Turns;
                    break;
                case GameManager.Turns.SECOND_USER:
                    if (string.IsNullOrEmpty(MainPage.mySettings.secondPlayerName))
                    {
                        this.ApplicationTitle.Text = AppResources.SecondPlayer + " " + AppResources.Turns;
                        break;
                    }
                    this.ApplicationTitle.Text = MainPage.mySettings.secondPlayerName + " " + AppResources.Turns;
                    break;
            }
        }

        private async void DoAIMove()
        {
            this.isAIThinking = true;
            await ((DependencyObject)this).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.DoAIMoveThread();
            });
        }

        private void DoAIMoveThread()
        {
            if (MainPage.TheGameManager.GetWhoTurns() != GameManager.Turns.AI)
                throw new Exception("Not the AI turns!");
            GomokuAI.Felismert felismert = new GomokuAI.Felismert();
            Move move;
            GameManager.MoveState aiMove = MainPage.TheGameManager.GetAIMove(out move, felismert);
            this.HandleGameState(aiMove, felismert);
            if (aiMove != GameManager.MoveState.AI_WIN)
                MainPage.DisplaySecondUserImage(this.imageList[move.X][move.Y]);
            this.isAIThinking = false;
            MainPage.PlayValidSound();
        }

        private void StartNewGame(bool autoLoad = true)
        {
            this.myUndoButton.IsEnabled = false;
            this.ContentPanelSrollViewer.ChangeView(null, this.ContentPanelSrollViewer.ScrollableHeight / 2.0, null);
            this.ContentPanelSrollViewer.ChangeView(this.ContentPanelSrollViewer.ScrollableWidth / 2.0, null, null);
            this.CreateTable(this.ContentPanel, MainPage.mySettings.X, MainPage.mySettings.Y, autoLoad);
            MainPage.TheGameManager.NewGame(MainPage.mySettings.gamingMode, MainPage.mySettings.startingPlayer,
                MainPage.mySettings.X, MainPage.mySettings.Y, MainPage.mySettings.AIStrength, MainPage.mySettings.Table, autoLoad);
            if (autoLoad && MainPage.mySettings.Turns != GameManager.Turns.NOBODY)
            {
                MainPage.TheGameManager.SetWhoTurns(MainPage.mySettings.Turns);
                MainPage.mySettings.Turns = GameManager.Turns.NOBODY;
            }
            if (MainPage.TheGameManager.GetWhoTurns() == GameManager.Turns.AI)
                this.DoAIMove();
            this.DisplayWhoTurns();
        }

        private void CreateTable(Grid target, int x, int y, bool autoLoad)
        {
            int[][] numArray = (int[][])null;
            if (autoLoad)
                numArray = MainPage.mySettings.Table;
            ((ICollection<ColumnDefinition>)target.ColumnDefinitions).Clear();
            ((ICollection<RowDefinition>)target.RowDefinitions).Clear();
            ((ICollection<UIElement>)((Panel)target).Children).Clear();
            this.imageList.Clear();
            for (int index = 1; index <= y; ++index)
            {
                ColumnDefinitionCollection columnDefinitions = target.ColumnDefinitions;
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
                ((ICollection<ColumnDefinition>)columnDefinitions).Add(columnDefinition);
            }
            for (int index = 1; index <= x; ++index)
            {
                RowDefinitionCollection rowDefinitions = target.RowDefinitions;
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
                ((ICollection<RowDefinition>)rowDefinitions).Add(rowDefinition);
            }
            for (int index1 = 0; index1 < ((ICollection<RowDefinition>)target.RowDefinitions).Count; ++index1)
            {
                List<Image> imageList = new List<Image>();
                for (int index2 = 0; index2 < ((ICollection<ColumnDefinition>)target.ColumnDefinitions).Count; ++index2)
                {
                    Image image1 = new Image();
                    imageList.Add(image1);
                    Image image2 = image1;

                    // Correctly attach the Tapped event handler using the += operator
                    image2.Tapped += this.Image_Tap;

                    if (numArray != null)
                    {
                        switch (numArray[index1][index2])
                        {
                            case 0:
                                MainPage.DisplayEmptyImage(image1);
                                break;
                            case 1:
                                MainPage.DisplayFirstUserImage(image1);
                                break;
                            case 2:
                                MainPage.DisplaySecondUserImage(image1);
                                break;
                        }
                    }
                    else
                        MainPage.DisplayEmptyImage(image1);
                    
                    image1.SetValue(Grid.RowProperty, (object)index1);
                    image1.SetValue(Grid.ColumnProperty, (object)index2);
                    ImageTag imageTag = new ImageTag();
                    image1.Tag = (object)imageTag;
                    imageTag.xCoordinate = index1;
                    imageTag.yCoordinate = index2;
                    target.Children.Add(image1);
                }
                this.imageList.Add(imageList);
            }
        }

        //Experimental
        private void Image_Tap(object sender, TappedRoutedEventArgs e)
        {
            this.HandleImageTap(sender);
        }

      

        private void myMagnifyAddImage_MouseLeftButtonUp(object sender, object e)
        {
            if (this.myZoomFactor >= 1.5)
                return;
            this.myZoomFactor += 0.25;
            this.myScaleingFactor.Text = " " + ((int)(this.myZoomFactor * 100.0)).ToString() + "% ";
            foreach (RowDefinition rowDefinition in (IEnumerable<RowDefinition>)this.ContentPanel.RowDefinitions)
                rowDefinition.Height = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
            foreach (ColumnDefinition columnDefinition in (IEnumerable<ColumnDefinition>)this.ContentPanel.ColumnDefinitions)
                columnDefinition.Width = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
        }

        private void myMagnifyMinusImage_MouseLeftButtonUp(object sender, object e)
        {
            if (this.myZoomFactor <= 0.5)
                return;
            this.myZoomFactor -= 0.25;
            this.myScaleingFactor.Text = " " + ((int)(this.myZoomFactor * 100.0)).ToString() + "% ";
            foreach (RowDefinition rowDefinition in (IEnumerable<RowDefinition>)this.ContentPanel.RowDefinitions)
                rowDefinition.Height = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
            foreach (ColumnDefinition columnDefinition in (IEnumerable<ColumnDefinition>)this.ContentPanel.ColumnDefinitions)
                columnDefinition.Width = new GridLength((double)MainPage.myIconSize * this.myZoomFactor);
        }

        private void LoadSettings(Settings theSettings)
        {
            switch (theSettings.gamingMode)
            {
                case GameManager.GamingMode.AgainstComputer:
                    this.myAgainstComputerRadioButton.IsChecked = new bool?(true);
                    break;
                case GameManager.GamingMode.TwoPlayer:
                    this.myTwoPlayerRadioButton.IsChecked = new bool?(true);
                    break;
            }
            switch (theSettings.startingPlayer)
            {
                case GameManager.StartingPlayer.Me:
                    this.myMeStartingRadioButton.IsChecked = new bool?(true);
                    break;
                case GameManager.StartingPlayer.Computer:
                    this.myCumputerStartingRadioButton.IsChecked = new bool?(true);
                    break;
                case GameManager.StartingPlayer.ByTurns:
                    this.myByTurnsStartingRadioButton.IsChecked = new bool?(true);
                    break;
            }
            switch (theSettings.tableSize)
            {
                case GameManager.TableSize.Small:
                    this.mySmallTableSizeRadioButton.IsChecked = new bool?(true);
                    break;
                case GameManager.TableSize.Medium:
                    this.myMediumTableSizeRadioButton.IsChecked = new bool?(true);
                    break;
                case GameManager.TableSize.Large:
                    this.myLargeTableSizeRadioButton.IsChecked = new bool?(true);
                    break;
            }
            this.myFirstPlayerNameTextBox.Text = theSettings.firstPlayerName;
            this.mySecondPlayerNameTextBox.Text = theSettings.secondPlayerName;
            this.aiSlider.Value = (double)theSettings.AIStrength;
            this.SetAIStrengthText();
            MainPage.DisplaySkin(this.mySkinImage, false);
        }

        // SetSettings
        private void SetSettings()
        {
            bool? isChecked = ((ToggleButton)this.myAgainstComputerRadioButton).IsChecked;
            bool flag1 = true;
            int num1 = isChecked.GetValueOrDefault() == flag1 ? (isChecked.HasValue ? 1 : 0) : 0;
            MainPage.mySettings.gamingMode = num1 == 0 ? GameManager.GamingMode.TwoPlayer : GameManager.GamingMode.AgainstComputer;
            isChecked = ((ToggleButton)this.myCumputerStartingRadioButton).IsChecked;
            bool flag2 = true;
            if ((isChecked.GetValueOrDefault() == flag2 ? (isChecked.HasValue ? 1 : 0) : 0) != 0)
            {
                MainPage.mySettings.startingPlayer = GameManager.StartingPlayer.Computer;
            }
            else
            {
                isChecked = ((ToggleButton)this.myMeStartingRadioButton).IsChecked;
                bool flag3 = true;
                int num2 = isChecked.GetValueOrDefault() == flag3 ? (isChecked.HasValue ? 1 : 0) : 0;
                MainPage.mySettings.startingPlayer = num2 == 0 ? GameManager.StartingPlayer.ByTurns : GameManager.StartingPlayer.Me;
            }
            isChecked = ((ToggleButton)this.mySmallTableSizeRadioButton).IsChecked;
            bool flag4 = true;
            if ((isChecked.GetValueOrDefault() == flag4 ? (isChecked.HasValue ? 1 : 0) : 0) != 0)
            {
                MainPage.mySettings.tableSize = GameManager.TableSize.Small;
            }
            else
            {
                isChecked = ((ToggleButton)this.myMediumTableSizeRadioButton).IsChecked;
                bool flag5 = true;
                int num3 = isChecked.GetValueOrDefault() == flag5 ? (isChecked.HasValue ? 1 : 0) : 0;
                MainPage.mySettings.tableSize = num3 == 0 ? GameManager.TableSize.Large : GameManager.TableSize.Large;
            }
            MainPage.mySettings.firstPlayerName = this.myFirstPlayerNameTextBox.Text;
            MainPage.mySettings.secondPlayerName = this.mySecondPlayerNameTextBox.Text;
            MainPage.mySettings.AIStrength = (int)((RangeBase)this.aiSlider).Value;
            MainPage.mySettings.selectedImageIndex = (((FrameworkElement)this.mySkinImage).Tag as SettingsImageTag).Index;
            MainPage.mySettings.selectedImageIsX = (((FrameworkElement)this.mySkinImage).Tag as SettingsImageTag).isX;
        }

        internal static void SaveGameState()
        {
            if (MainPage.TheGameManager != null && MainPage.TheGameManager.GetGameState() == GameManager.GameState.ACTIVE)
            {
                MainPage.mySettings.Table = MainPage.TheGameManager.GetTable();
                if (MainPage.mySettings.gamingMode == GameManager.GamingMode.TwoPlayer)
                    MainPage.mySettings.Turns = MainPage.TheGameManager.GetWhoTurns();
                else
                    MainPage.mySettings.Turns = GameManager.Turns.NOBODY;
            }
            else
                MainPage.mySettings.Turns = GameManager.Turns.MATCH_ENDED;
        }

        private void SetAIStrengthText()
        {
            this.aiTextBlock.Text = AppResources.AIStrength + ": " + this.GetAIStrength((int)(this.aiSlider).Value);
        }

        private string GetAIStrength(int value)
        {
            switch (value)
            {
                case 0:
                    return AppResources.AIVeryEasy;
                case 1:
                    return AppResources.AIEasy;
                case 2:
                    return AppResources.AINormal;
                case 3:
                    return AppResources.AIHard;
                case 4:
                    return AppResources.AIVeryHard;
                default:
                    return AppResources.AINormal;
            }
        }

        internal static async void PlayValidSound()
        {
            try
            {
                StorageFile storageFile = await Package.Current.InstalledLocation.GetFileAsync("Sounds\\valid.mp3");
                if (storageFile != null)
                {
                    IRandomAccessStream irandomAccessStream = await storageFile.OpenAsync(FileAccessMode.Read);
                    MediaElement mediaElement = new MediaElement();
                    mediaElement.SetSource(irandomAccessStream, storageFile.ContentType);
                    mediaElement.Volume = 0.7f;
                    mediaElement.Play();
                }
                storageFile = (StorageFile)null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] Sound error: " + ex.Message);
            }
        }


        // PlayInvalidSound
        internal static void PlayInvalidSound()
        {
        }

        internal static int GetDefaultSkinImageIndex() => 4;

        private static int GetNextSkinImageIndex(int index)
        {
            ++index;
            if (index > MainPage.MaxNumberOfImages)
                index = 1;
            return index;
        }

        private static void DisplayImage(Image image, string path)
        {
            ImageSource imageSource = (ImageSource)new BitmapImage(new Uri(path));
            ((DependencyObject)image).SetValue(Image.SourceProperty, (object)imageSource);
        }

        internal static void DisplayEmptyImage(Image image)
        {
            MainPage.DisplayImage(image, MainPage.SkinImagePrefix + "Empty" + (object)MainPage.mySettings.selectedImageIndex + MainPage.JPEG);
        }

        internal static void DisplayFirstUserImage(Image image)
        {
            MainPage.DisplayImage(image, MainPage.SkinImagePrefix + (MainPage.mySettings.selectedImageIsX 
                ? (object)"X"
                : (object)"O") + (object)MainPage.mySettings.selectedImageIndex + MainPage.JPEG);
            MainPage.DisplayOpacity(image);
        }

        private static void DisplayOpacity(Image image)
        {
            string str = (string)null;
            try
            {
                str = (image.Source as BitmapImage).UriSource.OriginalString;
            }
            catch
            {
            }
            if (str != null && (str.Contains("imageO1") || str.Contains("imageX1") || str.Contains("imageO3") || str.Contains("imageX3")))
                image.Opacity = 0.8;
            else
                image.Opacity = 0.95;
            if (MainPage.prevUserImage != null)
                MainPage.prevUserImage.Opacity = 1.0;
            MainPage.prevUserImage = image;
        }

        internal static void DisplaySecondUserImage(Image image)
        {
            MainPage.DisplayImage(image, MainPage.SkinImagePrefix + (MainPage.mySettings.selectedImageIsX
                ? (object)"O"
                : (object)"X") + (object)MainPage.mySettings.selectedImageIndex + MainPage.JPEG);
            MainPage.DisplayOpacity(image);
        }

        internal static void DisplayFirstUserWin(
          List<List<Image>> imglist,
          GomokuAI.Felismert felismert)
        {
            MainPage.DisplayWin(MainPage.SkinImagePrefix + (MainPage.mySettings.selectedImageIsX 
                ? (object)"X" 
                : (object)"O") + (object)MainPage.mySettings.selectedImageIndex + "_" + felismert.Direction.ToString() + MainPage.JPEG, imglist, felismert);
        }

        internal static void DisplaySecondUserWin(
          List<List<Image>> imglist,
          GomokuAI.Felismert felismert)
        {
            MainPage.DisplayWin(MainPage.SkinImagePrefix + (MainPage.mySettings.selectedImageIsX
                ? (object)"O"
                : (object)"X") + (object)MainPage.mySettings.selectedImageIndex + "_" + felismert.Direction.ToString() + MainPage.JPEG, imglist, felismert);
        }

        private static void DisplayWin(
          string imagePath,
          List<List<Image>> imageList,
          GomokuAI.Felismert felismert)
        {
            int startX = felismert.StartX;
            int startY = felismert.StartY;
            for (int index = 0; index < 5; ++index)
            {
                MainPage.DisplayImage(imageList[startX][startY], imagePath);
                switch (felismert.Direction)
                {
                    case 0:
                        ++startX;
                        break;
                    case 1:
                        ++startY;
                        break;
                    case 2:
                        ++startX;
                        ++startY;
                        break;
                    case 3:
                        --startX;
                        ++startY;
                        break;
                }
            }
        }

        internal static void DisplaySkin(Image skinImage, bool isNext = true)
        {
            if (((FrameworkElement)skinImage).Tag == null || !(((FrameworkElement)skinImage).Tag is SettingsImageTag))
            {
                string path = MainPage.SkinImagePrefix + (MainPage.mySettings.selectedImageIsX ? "X" : "O") 
                    + MainPage.mySettings.selectedImageIndex.ToString() + MainPage.JPEG;
                MainPage.DisplayImage(skinImage, path);
                skinImage.Tag = ((object)new SettingsImageTag()
                {
                    Index = MainPage.mySettings.selectedImageIndex,
                    isX = MainPage.mySettings.selectedImageIsX
                });
            }
            else
            {
                SettingsImageTag tag = ((FrameworkElement)skinImage).Tag as SettingsImageTag;
                if (isNext)
                {
                    if (tag.isX)
                    {
                        tag.isX = false;
                        tag.Index = MainPage.GetNextSkinImageIndex(tag.Index);
                        MainPage.DisplayImage(skinImage, MainPage.SkinImagePrefix + MainPage.O + tag.Index.ToString() + MainPage.JPEG);
                    }
                    else
                    {
                        tag.isX = true;
                        MainPage.DisplayImage(skinImage, MainPage.SkinImagePrefix + MainPage.X + tag.Index.ToString() + MainPage.JPEG);
                    }
                }
                else
                    MainPage.DisplayImage(skinImage, MainPage.SkinImagePrefix +
                        (tag.isX ? MainPage.X : MainPage.O) + tag.Index.ToString() + MainPage.JPEG);
            }
        }//DisplaySkin



        // **** Bottom buttons handling ****


        // ** Button handling **
        // myNewGameButton_Click
        private void myNewGameButton_Click(object sender, object e)
        {
            this.myNewGameButton.IsEnabled = false;
            this.StartNewGame(false);
            this.myNewGameButton.IsEnabled = true;

        }

        // myUndoButton_Click
        private void myUndoButton_Click(object sender, object e)
        {
            this.myUndoButton.IsEnabled = false;
            Move undoAIMove;
            Move undoUserMove;
            MainPage.TheGameManager.Undo(out undoAIMove, out undoUserMove);
            if (undoAIMove != null)
            {
                Image image = this.imageList[undoAIMove.X][undoAIMove.Y];
                MainPage.DisplayEmptyImage(image);
                image.Opacity = 1.0;
            }
            if (undoUserMove != null)
                MainPage.DisplayEmptyImage(this.imageList[undoUserMove.X][undoUserMove.Y]);
            this.DisplayWhoTurns();
        }

        // mySettingsButton_Click
        private void mySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (this.myMainPageUIIsInSettingsMode)
            {
                this.SetSettings();
                this.SettingsGrid.Visibility = Visibility.Collapsed;
                this.ContentPanelSrollViewer.Visibility = Visibility.Visible;
                this.myNewGameButton.Visibility = Visibility.Visible;
                this.myUndoButton.Visibility = Visibility.Visible;
                
                this.StartNewGame(false);
            }
            else
            {
                this.LoadSettings(MainPage.mySettings);
                this.SettingsGrid.Visibility = Visibility.Visible;
                this.ContentPanelSrollViewer.Visibility = Visibility.Collapsed;
                this.myNewGameButton.Visibility = Visibility.Collapsed;
                this.myUndoButton.Visibility = Visibility.Collapsed;
            }
            this.myMainPageUIIsInSettingsMode = !this.myMainPageUIIsInSettingsMode;*/
        }

        
        private async void myReviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = await Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName)) ? 1 : 0;
            }
            catch
            {
            }
        }

        /*private async void myRemoveAdsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num = await Launcher.LaunchUriAsync(new Uri("ms-windows-store:PDP?PFN=40811eyack.com.560824E22F025_xsbsxxypt8dh6")) ? 1 : 0;
            }
            catch
            {
            }
        }*/



        //Experimental
        private void mySkinImage_MouseLeftButtonUp(object sender, TappedRoutedEventArgs e)
        {
            MainPage.DisplaySkin(this.mySkinImage);
        }

        // 
        private void aiSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            this.aiSlider.Value = (double)(int)Math.Round(e.NewValue);
            this.SetAIStrengthText();
        }

      
    }
}
