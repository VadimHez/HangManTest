using System.Collections.Generic;

namespace HangManServices
{
    public class HangManModel
    {
        public HangManStatus HangManStatus { get; set; }

        public PlayerStatus PlayerStatus { get; set; }

        public char[] AnswearChars { get; set; }

        public List<char> EnteredChars { get; set; }

        public string Errors { get; set; }
    }
}
