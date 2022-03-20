using Main;

// See https://aka.ms/new-console-template for more information
Console.WriteLine();


/// Zad 1 - RAIL FENCE
Console.WriteLine("Testing RailFence.");
string Zad1_0_word = "CRYPTOGRAPHY";
string Zad1_1_word = "ABCDEFGHIJKL";

string Zad1_1_word_encrypted = new RailFence(railCount: 3).Encrypt(Zad1_1_word);
string Zad1_1_word_encrypted_decrypted = new RailFence(railCount: 3).Decrypt(Zad1_1_word_encrypted);


/// Zad 2 - PRZESTAWIENIA MACIERZOWE
string Zad2_0_word = "CRYPTOGRAPHYOSA";
string Zad2_1_word = "ABCDEFGHIJKLMN";
string Zad2_2_word = "ABCDEFGHIJKLMNOPRSTUWVXYZ";
// int[] Zad2_0_key = new int[]{3, 4, 1, 5, 2};
int[] Zad2_0_key = new int[]{3, 1, 4, 2};

string Zad2_1_word_encrypted = new MatrixShift(key: Zad2_0_key).Encrypt(Zad2_2_word);
Console.WriteLine(Zad2_1_word_encrypted);
string Zad2_1_word_encrypted_decrypted = new MatrixShift(key: Zad2_0_key).Decrypt(Zad2_1_word_encrypted);
Console.WriteLine(Zad2_1_word_encrypted_decrypted);

