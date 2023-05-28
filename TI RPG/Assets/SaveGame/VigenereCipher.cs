namespace SaveGame
{
    public static class VigenereCipher
    {
        // Chave utilizada para criptografar e descriptografar
        private const string encryptionKey = "MY_ENCRYPTION_KEY";

        public static string Encrypt(string input)
        {
            string encryptedText = "";
            int keyIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                int keyChar = encryptionKey[keyIndex % encryptionKey.Length];

                if (char.IsLetter(currentChar))
                {
                    char encryptedChar = (char)((currentChar + keyChar - 2 * 'A') % 26 + 'A');
                    encryptedText += encryptedChar;
                }
                else
                {
                    encryptedText += currentChar;
                }

                keyIndex++;
            }

            return encryptedText;
        }

        public static string Decrypt(string input)
        {
            string decryptedText = "";
            int keyIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                int keyChar = encryptionKey[keyIndex % encryptionKey.Length];

                if (char.IsLetter(currentChar))
                {
                    char decryptedChar = (char)((currentChar - keyChar + 26) % 26 + 'A');
                    decryptedText += decryptedChar;
                }
                else
                {
                    decryptedText += currentChar;
                }

                keyIndex++;
            }

            return decryptedText;
        }
    }
}