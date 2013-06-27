using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;
using Windows.Storage;
using System.Windows.Resources;
using System.IO;
using System.Text;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            List<PuzzleWord> puzzles = CreatePuzzleList();
            List<GrouppedList<PuzzleWord>> grouppedPuzzles= GrouppedList<PuzzleWord>.CreateGroups(puzzles, puzzleWord => puzzleWord.Type);
            PuzzleList.ItemsSource = grouppedPuzzles;
            //PuzzleList.DataContext = grouppedPuzzles;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private List<PuzzleWord> CreatePuzzleList()
        {
            List<PuzzleWord> result = new List<PuzzleWord>();

            StreamResourceInfo resourceInfo = Application.GetResourceStream(new Uri("PuzzleWords.txt", UriKind.Relative));
            using(StreamReader sr = new StreamReader(resourceInfo.Stream))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string question = "", answer = "", hint = "";
                    if (line != null)
                    {
                        string[] words = line.Split(new char[] { ' ' });
                        if (words.Count() != 2)
                        {
                            continue;
                        }
                        else
                        {
                            question = words[0];
                            hint = words[1];
                        }
                    }
                    line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else
                    {
                        answer = line;
                    }
                    PuzzleWord puzzleWord = CreatePuzzleWord(question, answer, hint);
                    result.Add(puzzleWord);
                }
            }

            return result;
        }

        private PuzzleWord CreatePuzzleWord(string question, string answer, string hint)
        {
            string type = "";
            string[] typeList = new string[] { "字", "动物", "地名", "成语" };
            foreach (var t in typeList)
            {
                if (hint.Contains(t))
                {
                    type = t;
                }
            }

            if(type == "")
            {
                type = "Other";
            }

            if (question.Length > 15)
            {
                question = FormatQuestion(question);
            }
            return new PuzzleWord(question, answer, type, hint);
        }

        private string FormatQuestion(string question)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var l in question)
            {
                sb.Append(l);
                if (i >= 13 && i % 13 == 0)
                {
                    sb.Append("\r\n");
                }
                i++;
            }
            return sb.ToString();
        }


        private void PuzzleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PuzzleWord pw = PuzzleList.SelectedItem as PuzzleWord;
            MessageBox.Show(pw.Answer);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}