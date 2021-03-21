using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HangManServices
{
    public class HangManService : IHangManService
    {
        private HangManModel _hangManModel;
        private string _secretPhrase;
        private char[] _leavedChars;
        public HangManService()
        {
            _hangManModel = new HangManModel();
        }
        private void Init(string secret)
        {
            try
            {
                _hangManModel.PlayerStatus = PlayerStatus.Playing;
                _hangManModel.HangManStatus = HangManStatus.Clear;
                _hangManModel.AnswearChars = Enumerable.Repeat('_', _secretPhrase.Length).ToArray();
                _hangManModel.EnteredChars = new List<char>();
                _leavedChars = secret.ToUpper().ToCharArray();
            }
            catch (Exception e)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                _hangManModel.Errors = $"{method.ReflectedType.Name}, {method.Name}, error: {e.Message}";
            }
        }

        public void SetSecret(string secret) 
        {
            _secretPhrase = secret;
            Init(secret);
        }

        public HangManModel NextStep(char? enteredOrig = null)
        {
            if (enteredOrig == null)
                return _hangManModel;

            try
            {
                char enteredValue = char.ToUpper((char)enteredOrig);
                _hangManModel.EnteredChars.Add(enteredValue);

                if (_leavedChars.Contains(enteredValue))
                {
                    _leavedChars = _leavedChars.Where(value => value != enteredValue && value != ' ').ToArray();
                    for (int i = 0; i < _secretPhrase.Length; i++)
                    {
                        if ((char)enteredOrig == _secretPhrase[i])
                        {
                            _hangManModel.AnswearChars[i] = (char)enteredOrig;
                        }
                    }
                }
                else
                {
                    if (_hangManModel.HangManStatus != HangManStatus.TwoLegs)
                        _hangManModel.HangManStatus++;
                    else
                        _hangManModel.PlayerStatus = PlayerStatus.Lose;
                }

                if (_leavedChars.Length == 0)
                    _hangManModel.PlayerStatus = PlayerStatus.Win;

            }
            catch (Exception e)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                _hangManModel.Errors = $"{method.ReflectedType.Name}, {method.Name}, error: {e.Message}";
            }

            return _hangManModel;
           
        }

        public bool IsGameFinished()
        {
            return _hangManModel.PlayerStatus == PlayerStatus.Lose || _hangManModel.PlayerStatus == PlayerStatus.Win;
        }
    }
}
