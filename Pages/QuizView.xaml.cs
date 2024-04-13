/*

Program Author: Kennide Allen

USM ID: 10138082

Assignment: Program 3: Capital Quiz, Part 3

Description:

Handles the functionality of the Quiz and allows the user to quit during the middle of the quiz. 
Displays the question, users current score, and whether their answer is correct or incorrect.
*/

using Lab3CapitalQuizPart3.Classes;
namespace Lab3CapitalQuizPart3.Pages;

public partial class QuizView : ContentPage
{
    private int _score = 0;
	private int _currentQuestion = 1;
    private int _totalQuestions = 20;
    private string? _selectedOption;
    private State? _state;
    private  Quiz _quiz = new();
    private  List<State> _missedQuestions = new();
    private bool _hasSubmitted = false;


    public QuizView()
	{
		InitializeComponent();
        DisplayQuestion();
    }

    // displays the question, answer choices, and submit button text
	private void DisplayQuestion()
	{
        QuizQuestion? question = _quiz.FetchNext();

        if (question != null && question.Correct != null)
        {
            _state = question.Correct;

            // Question
            lblQuestion.Text = "What is the capital of " + _state.StateName + "?";

            // list that holds the options
            List<State> _options = question.GenerateOptions();

            if (_options.Count >= 4)
            {
                rdoChoice1.Content = _options[0].CapitalName;
                rdoChoice2.Content = _options[1].CapitalName;
                rdoChoice3.Content = _options[2].CapitalName;
                rdoChoice4.Content = _options[3].CapitalName;
            }

            _hasSubmitted = false;
            SubmitBtn.Text = "Submit";
        }
    }

    private void SelectedOption(object sender, CheckedChangedEventArgs e)
    {
        // if a radio button is selected, change it to a string and enable the submit button
        if (sender is RadioButton selectedRadio && selectedRadio.IsChecked)
        {
            _selectedOption = selectedRadio.Content.ToString();
            SubmitBtn.IsEnabled = true;
            SubmitBtn.BackgroundColor = Color.FromArgb("#AC99EA");
        }
        else
            SubmitBtn.IsEnabled = false;
    }

    private void SubmitBtnClicked(object sender, EventArgs e)
    {
        string? selectedOption = _selectedOption;

        // if an answer has been submitted, check if there are more questions
        // otherwise, check the user answer answer
        if (_hasSubmitted)
        {
            if (_currentQuestion < _quiz.Count)
            {
                _currentQuestion++;

                AnswerReset();
                DisplayQuestion();
            }
            else
            {
                SubmitBtn.BackgroundColor = Color.FromArgb("#512BD4");

                if (App.Current != null)
                {
                    App.Current.MainPage = new ResultsView(_score, _totalQuestions, _missedQuestions);
                }
            }
        }
        else
        {            
            if (selectedOption != null)
            {
                if (_state != null)
                {
                    if (selectedOption == _state.CapitalName)
                    {
                        _score++;
                        lblScoreCount.Text = _score.ToString() + " / " + _quiz.Count.ToString();

                        lblAnswerStatus.Text = "Correct!";
                        imgAnswerStatus.Source = "icon_correct.png";

                    }
                    else
                    {
                        lblAnswerStatus.Text = "Incorrect...";
                        imgAnswerStatus.Source = "icon_incorrect.png";


                        // add to missed quesions list
                        _missedQuestions.Add(_state);
                    }
                }
            }

            if (_currentQuestion != _quiz.Count)
                SubmitBtn.Text = "Next";
            else
                SubmitBtn.Text = "Results";
            _hasSubmitted = true;
        }
    }

    // resets the radio buttons and answer message and image
    private void AnswerReset()
    {
        rdoChoice1.IsChecked = false;
        rdoChoice2.IsChecked = false;
        rdoChoice3.IsChecked = false;
        rdoChoice4.IsChecked = false;

        lblAnswerStatus.Text = null;
        imgAnswerStatus.Source = null;
    }

    private void QuitBtnClicked(object sender, EventArgs e)
    {
        App.Current?.Quit();
    }
}