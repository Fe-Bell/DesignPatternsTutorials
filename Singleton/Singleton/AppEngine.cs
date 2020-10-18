using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singleton
{
    /// <summary>
    /// Sample class to illustrate singleton.
    /// </summary>
    public class AppEngine
    {
        private static AppEngine appEngine = null;

        /// <summary>
        /// Returns a singleton instance of this class via property.
        /// </summary>
        public static AppEngine Default
        {
            get
            {
                if(appEngine is null)
                {
                    appEngine = new AppEngine();
                }

                return appEngine;
            }
        }

        /// <summary>
        /// A random ID for this class.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Sample class to illustrate singleton.
        /// </summary>
        private AppEngine()
        {
            Random random = new Random();
            ID = random.Next(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// Replaces a specific char in a string.
        /// </summary>
        /// <param name="input">String to be altered.</param>
        /// <param name="charToReplace">A char to be inserted.</param>
        /// <param name="index">The index of the char to be removed.</param>
        /// <returns>True if success. False otherwise.</returns>
        private bool TrySwapChar(ref string input, char charToReplace, int index)
        {
            if(!string.IsNullOrEmpty(input) && input.Length > 0 && input.Length > index)
            {
                input = input.Remove(index, 1);
                input = input.Insert(index, charToReplace.ToString());

                return true;
            }

            return false;
        }
        /// <summary>
        /// Calculates one's complement of a binary string.
        /// </summary>
        /// <param name="input">A binary string.</param>
        /// <returns>True if success. False otherwise.</returns>
        private bool TryOnesComplementFromBinaryString(ref string input)
        {
            if (input != null && input.Length > 0)
            {
                //One's complement
                for (int i = 0; i < input.Length; i++)
                {
                    var currChar = input[i] != '0' ? '0' : '1';
                    TrySwapChar(ref input, currChar, i);
                }

                return true;
            }

            return false;
        }
        /// <summary>
        /// Calculates the two's complement of a binary string.
        /// </summary>
        /// <param name="input">A binary string.</param>
        /// <returns>True if success. False otherwise.</returns>
        private bool TryTwosComplementFromBinaryString(ref string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                /*Two's complement - adds one to last bit and handles carry over.*/
                bool hasCarry = true;
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (input[i] == '1' && hasCarry)
                    {
                        TrySwapChar(ref input, '0', i);
                    }
                    else if (input[i] == '0' && hasCarry)
                    {
                        TrySwapChar(ref input, '1', i);
                        hasCarry = false;
                    }
                    /*Null terminator and no carry*/
                    else
                    {
                        /*Do nothing!*/
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a singleton instance of this class via method.
        /// </summary>
        /// <returns>A static instance of this class.</returns>
        public static AppEngine GetDefault()
        {
            if (appEngine is null)
            {
                appEngine = new AppEngine();
            }

            return appEngine;
        }
        /// <summary>
        /// Converts a 32bit integer to a binary using string manipulation.
        /// </summary>
        /// <param name="value">An int.</param>
        /// <returns>A 32-char binary string. If negative uses the two's complement to represent result.</returns>
        public string GetBinary(int value)
        {
            var binary = Convert.ToString(Math.Abs(value), 2);

            //One's complement
            if(!TryOnesComplementFromBinaryString(ref binary))
            {
                throw new Exception("Could not get one's complement for the specified input");
            }

            //Two's complement
            if(!TryTwosComplementFromBinaryString(ref binary))
            {
                throw new Exception("Could not get two's complement for the specified input");
            }

            return binary.PadLeft(/*size in bytes*/ sizeof(int) * /*bits per byte*/ 8, /*Completes with 0s or 1s*/ value > 0 ? '0' : '1');
        }
    }
}
