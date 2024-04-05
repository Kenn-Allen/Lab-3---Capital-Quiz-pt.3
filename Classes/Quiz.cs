using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3CapitalQuizPart3.Classes
{
    public class State
    {
        public string StateName { get; set; } = "";
        public string CapitalName { get; set; } = "";
    }


    class QuizQuestion
    {
        private State? _correct = null;
        private List<State> _distractors = new List<State>();

        public QuizQuestion(State? correct = null)
        {
            _correct = correct;

            if (correct != null)
                SetDistractors();
        }

        public State? Correct
        {
            get
            {
                return _correct;
            }
            set
            {
                _correct = value;
                SetDistractors();
            }
        }

        public List<State> GenerateOptions()
        {
            if (_correct == null)
                return null;

            List<State> options = new List<State>();
            options.Add(_correct);
            options.AddRange(_distractors);

            Shuffle(options, 10);

            return options;
        }
    

    private void Shuffle(List<State> options, int passes)
    {
        //Shuffle the List.
        Random random = new Random();
        for (int i = 0; i < passes; i++)
        {
            int x = random.Next(options.Count);
            int y = random.Next(options.Count);
            State temp = options[x];
            options[x] = options[y];
            options[y] = temp;
        }
    }

    private void SetDistractors()
    {
        if (_correct == null)
            return;

        //called when the correct state is changed.
        List<State> copy = new List<State>(App.states);
        copy.Remove(_correct);
        List<State> final = new List<State>();
        Random r = new Random();

        for (int i = 0; i < 3; i++)
        {
            int chosen = r.Next(0, copy.Count);
            final.Add(copy[chosen]);
            copy.RemoveAt(chosen);
        }

        _distractors = final;
    }
}

class Quiz
{
    public Queue<QuizQuestion> questionQueue = new Queue<QuizQuestion>();
    int _count;

    public Quiz(int size = 20)
    {
        _count = size;
        GenerateNewQuiz(size);
    }

    public void GenerateNewQuiz(int size)
    {
        questionQueue.Clear();
        List<State> copy = new(App.states);
        Random r = new Random();
        for (int i = 0; i < size; i++)
        {
            int index = r.Next(0, copy.Count);
            State state = copy[index];
            copy.RemoveAt(index);

            QuizQuestion question = new QuizQuestion { Correct = state };
            questionQueue.Enqueue(question);

        }
    }

    public QuizQuestion? FetchNext()
    {
        if (questionQueue.Count == 0)
            return null;
        else
            return questionQueue.Dequeue();
    }

    public int Count { get { return _count; } }
}
}
