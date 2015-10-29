using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Simulator
{
    public class Scanner
    {
        private string[] _barcodes;
        private int _index;
        private TextBoxBase _target;

        public string[] Barcodes { get { return _barcodes; } set { _barcodes = value; } }

        private Scanner()
        {
            this._index = 0;
            this._target = null;
        }
        /// <summary>
        /// When ypu want to try a set of barcodes
        /// </summary>
        /// <param name="barcodes">set of bar codes</param>
        /// <param name="barcodes">Input text where codes will be set</param>
        public Scanner(string[] barcodes, TextBoxBase target)
            : this()
        {
            this._barcodes = barcodes;
            this._target = target;
        }

        public void NextScan()
        {
            string BarCode = GetBarCode(_index);
            _index++;
            PushCode(_target, BarCode);
        }

        public void RandomScan()
        {
            Random Rnd = new Random();
            int NewIndex = Rnd.Next(0, _barcodes.Length - 1);
            PushCode(_target, GetBarCode(NewIndex));
        }

        public static void RandomBarCodeScan(TextBoxBase target)
        {
            Random Rnd = new Random();
            int RandomBarCode = Rnd.Next(0, 1000000);
            PushCode(target, RandomBarCode.ToString().PadLeft(7, '0'));
        }

        private string GetBarCode(int index)
        {
            if (_barcodes != null)
            {
                if (index >= _barcodes.Length)
                    index = 0;

                _index = index;
                return _barcodes[_index];
            }

            return string.Empty;
        }

        private static void PushCode(TextBoxBase target, string barcode)
        {
            target.Focus();

            foreach (char Character in barcode)
                SendKeys.SendWait(Character.ToString());

            SendKeys.Send("{ENTER}");
        }
    }
}
