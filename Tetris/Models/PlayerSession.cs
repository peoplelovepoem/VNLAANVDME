using System;
using System.Linq;

namespace Tetris.Models
{
    public sealed class PlayerSession
    {
        public string PlayerName
        {
            get; private set;
        }

        public static bool TryValidateName(string input, out string name, out string error)
        {
            name = (input ?? "").Trim();
            error = null;
            if (input != null && input.Any(char.IsControl))
                error = "Tên không được chứa ký tự điều khiển.";
            else if (name.Length < 1 || name.Length > 20)
                error = "Vui lòng nhập tên từ 1 đến 20 ký tự.";
            else if (name.Any(c => !char.IsLetterOrDigit(c)))
                error = "Tên chỉ được chứa chữ và số, không có dấu cách.";
            return error == null;
        }

        public void SetName(string input)
        {
            string name, error;
            if (!TryValidateName(input, out name, out error))
                throw new ArgumentException(error, "input");
            PlayerName = name;
        }
    }
}
