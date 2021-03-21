using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HangManServices.Tests
{
    [TestClass]
    public class HangManServiceTest
    {
        private IHangManService _service;
        private const string _secretValue = "Some text";
        private HangManModel _modelInit;

        public HangManServiceTest()
        {
            _service = new HangManService();
            _service.SetSecret(_secretValue);
            _modelInit = new HangManModel()
            {
                EnteredChars = new List<char>(),
                HangManStatus = HangManStatus.Clear,
                PlayerStatus = PlayerStatus.Playing,
                AnswearChars = new char[] { '_', '_', '_', '_', '_', '_', '_', '_', '_' }
            };
        }

        [TestMethod]
        public void NextStep_withDefaultParamShouldReturnModelWithInitValues()
        {
            var model = _service.NextStep();
            Assert.AreEqual(_modelInit.AnswearChars.Length, model.AnswearChars.Length);
            Assert.AreEqual(_modelInit.EnteredChars.Count, model.EnteredChars.Count);
            Assert.AreEqual(_modelInit.HangManStatus, model.HangManStatus);
            Assert.AreEqual(_modelInit.PlayerStatus, model.PlayerStatus);
        }

        [TestMethod]
        public void NextStep_withExistingChar()
        {
            char enteredChar = 'S';
            var model = _service.NextStep(enteredChar);
            Assert.AreEqual(1, model.EnteredChars.Count);
            Assert.AreEqual(enteredChar, model.EnteredChars[0]);
            Assert.IsTrue(model.AnswearChars.Contains(enteredChar));
            Assert.AreEqual(_modelInit.HangManStatus, model.HangManStatus);
            Assert.AreEqual(_modelInit.PlayerStatus, model.PlayerStatus);
        }

        [TestMethod]
        public void NextStep_withNotExistingChar()
        {
            char enteredChar = 'Z';
            var model = _service.NextStep(enteredChar);
            Assert.AreEqual(1, model.EnteredChars.Count);
            Assert.AreEqual(enteredChar, model.EnteredChars[0]);
            Assert.IsFalse(model.AnswearChars.Contains(enteredChar));
            Assert.AreEqual(HangManStatus.Stick, model.HangManStatus);
            Assert.AreEqual(_modelInit.PlayerStatus, model.PlayerStatus);
        }

        [TestMethod]
        public void NextStep_CallingForWin()
        {
            char[] enteredChars = new char[] { 's', 'o', 'm', 'e', 't', 'x', 't' };
            HangManModel model = new HangManModel();
            foreach (char item in enteredChars)
            {
                model = _service.NextStep(item);
            }
            Assert.AreEqual(PlayerStatus.Win, model.PlayerStatus);
        }

        [TestMethod]
        public void NextStep_CallingForLose()
        {
            char[] enteredChars = new char[] { 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z' };
            HangManModel model = new HangManModel();
            foreach (char item in enteredChars)
            {
                model = _service.NextStep(item);
            }
            Assert.AreEqual(HangManStatus.TwoLegs, model.HangManStatus);
            Assert.AreEqual(PlayerStatus.Lose, model.PlayerStatus);
        }
    }
}
