using Lab3CapitalQuizPart3.Classes;

namespace Lab3CapitalQuizPart3.Pages;

public partial class QuizView : ContentPage
{
	private int _score = 0;
	private int _currentQuestion = 0;

    Quiz quiz = new Quiz(size: 20);

    public QuizView()
	{
		InitializeComponent();
        DisplayQuestion();

		imgIncorrect.Source = "icon_incorrect.png";
    }

	private void DisplayQuestion()
	{
        QuizQuestion? question = quiz.FetchNext();

        if (question != null)
        {
            var state = question.Correct.StateName;
            var capital = question.Correct.CapitalName;

            // Question
            lblQuestion.Text = "What is the capital of " + state + "?";

            List<State> _options = question.GenerateOptions();

            if (_options.Count >= 4)
            {
                radChoice1.Content = _options[0].CapitalName;
                radChoice2.Content = _options[1].CapitalName;
                radChoice3.Content = _options[2].CapitalName;
                radChoice4.Content = _options[3].CapitalName;
            }
        }
    }
}