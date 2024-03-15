using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _4.Model
{
    public class Model
    {
        //в модели не должно быть reader
        private FileReader _reader = new FileReader("words.json");
        private string _word;
        private List<char> _openedLetters = new List<char>();
        private string _hint;
        private int _errorsNuber = 0;//опечатка
        private int _openedLettersCount = 0;

        public delegate void OpenLetterHandler(char ch);
        public event OpenLetterHandler OpenLetterEvent;

        public delegate void InitializeNewWordHandler();
        public event InitializeNewWordHandler InitializeNewWordEvent;

        public delegate void ErrorLetterHandler(char ch);
        public event ErrorLetterHandler ErrorLetterEvent;

        public delegate void EndGameHandler(bool isWin);
        public event EndGameHandler EndGameEvent;


        public Model() 
        {
        }

        public void InitializeNewWord()
        {
            _reader.ReadFromFile();

            _word = _reader.GetWord();
            _hint = _reader.GetHint();
            _openedLetters.Clear();
            _errorsNuber = 0;
            _openedLettersCount = 0;
            InitializeNewWordEvent.Invoke();
        }

        public void OpenLetter(char ch)
        {
            if(_openedLetters.Contains(ch))
            {
                return;
            }

            if(_word.Contains(Char.ToLower(ch)))
            {
                _openedLetters.Add(ch);
                OpenLetterEvent.Invoke(Char.ToLower(ch));
                _openedLettersCount += _word.Count(x => x == Char.ToLower(ch));
                if (_openedLettersCount == _word.Length)
                {
                    EndGameEvent.Invoke(true);
                }

                return;
            }
            _errorsNuber++;
            ErrorLetterEvent(ch);
            if(_errorsNuber == 7)
            {
                EndGameEvent.Invoke(false);
            }
        }

        public string GetWord()
        {
            return _word;
        }
    
        public string GetHint()
        {
            return _hint;
        } 

        public int GetErrorsNumber()
        {
            return _errorsNuber;
        }
    }

}
