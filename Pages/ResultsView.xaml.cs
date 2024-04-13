using Lab3CapitalQuizPart3.Classes;
using System.Collections.ObjectModel;

namespace Lab3CapitalQuizPart3.Pages;

public partial class ResultsView : ContentPage
{
    private int _score;
    private int _totalQuestions;
    private List<State> _missedStates;
    class MissedStateView     
    { 
        string _text = ""; 
        public string Text
        { 
            get 
            { 
                return _text; 
            } 
            set 
            { 
                _text = value; 
            } 
        } 
    }

    public ResultsView(int score, int totalQuestions, List<State> missedStates)
    {
        _score = score;
        _totalQuestions = totalQuestions;
        _missedStates = missedStates;

		InitializeComponent();
        Results();
	}

    private void ListMissedStates(List<State> missedStates) 
    { 
        ObservableCollection<MissedStateView> missedStatesStr = new ObservableCollection<MissedStateView>(); 
        foreach (State state in missedStates) 
        { 
            string output = $"{state.CapitalName}, {state.StateName}"; 
            missedStatesStr.Add(new MissedStateView { Text = output }); 
        } 
        
        collMissedStates.ItemsSource = missedStatesStr; 
    }

    private void Results()
    {
        lblScoreCount.Text = _score.ToString();

        if (_score < _totalQuestions)
        {
            lblWinLoseMsg.Text = "You missed the following states:";
            ListMissedStates(_missedStates);
            collMissedStates.IsVisible = true;
        }
        else
        {
            lblWinLoseMsg.Text = "You got all questions correct!";
            imgMerica.IsVisible = true;
        }
    }

    private void NewGameBtnClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new QuizView();
    }

    private void ExitBtnClicked(object sender, EventArgs e)
    {
         if (App.Current != null)
            App.Current.Quit();
    }
}