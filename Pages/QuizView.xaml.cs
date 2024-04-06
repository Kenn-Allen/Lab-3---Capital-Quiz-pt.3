using Lab3CapitalQuizPart3.Classes;

namespace Lab3CapitalQuizPart3.Pages;

public partial class QuizView : ContentPage
{
	private int _score = 0;
	private int _currentQuestion = 0;
    private string? _selectedOption;
    private string? _state;
    private string? _capital;

    Quiz quiz = new();

    public QuizView()
	{
		InitializeComponent();
        DisplayQuestion();
    }

	private void DisplayQuestion()
	{
        QuizQuestion? question = quiz.FetchNext();

        if (question != null && question.Correct != null)
        {
            var state = question.Correct.StateName;
            _state = state;

            var capital = question.Correct.CapitalName;
            _capital = capital;

            // Question
            lblQuestion.Text = "What is the capital of " + state + "?";

            List<State> _options = question.GenerateOptions();

            if (_options.Count >= 4)
            {
                rdoChoice1.Content = _options[0].CapitalName;
                rdoChoice2.Content = _options[1].CapitalName;
                rdoChoice3.Content = _options[2].CapitalName;
                rdoChoice4.Content = _options[3].CapitalName;
            }
        }
    }

    private void SelectedOption(object sender, CheckedChangedEventArgs e)
    {
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

        if (selectedOption != null)
        {
            if (_state != null && _capital != null)
            {
                string correctCapital = _capital;

                if (selectedOption == correctCapital)
                {
                    _score++;
                    lblScoreCount.Text = _score.ToString() + " / " + quiz.Count.ToString();

                    lblAnswerStatus.Text = "Correct!";
                    imgAnswerStatus.Source = "icon_correct.png";
                }
                else
                {
                    lblAnswerStatus.Text = "Incorrect...";
                    imgAnswerStatus.Source = "icon_incorrect.png";

                    // add to wrong answers
                }
            }
        }

        SubmitBtn.IsVisible = false;
        NextResultBtn.IsVisible = true;
    }

    private void AnswerReset()
    {
        rdoChoice1.IsChecked = false;
        rdoChoice2.IsChecked = false;
        rdoChoice3.IsChecked = false;
        rdoChoice4.IsChecked = false;

        lblAnswerStatus.Text = null;
        imgAnswerStatus.Source = null;
    }
    
    private void NextResultBtnClicked(object sender, EventArgs e)
    {
        _currentQuestion++;

        AnswerReset();

        if (_currentQuestion < quiz.questionQueue.Count)
        {
            DisplayQuestion();

            NextResultBtn.IsVisible = false;
            SubmitBtn.IsVisible = true;
        }
        else
        {
            NextResultBtn.Text = "Results";
            NextResultBtn.BackgroundColor = Color.FromArgb("#512BD4");

            if (App.Current != null)
                App.Current.MainPage = new ResultsView();
        }
    }

    private void QuitBtnClicked(object sender, EventArgs e)
    {
        if (App.Current != null)
            App.Current.Quit();
    }
}